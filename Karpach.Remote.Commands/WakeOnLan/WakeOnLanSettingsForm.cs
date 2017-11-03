using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Karpach.Remote.Commands.WakeOnLan
{
    public partial class WakeOnLanSettingsForm : Form
    {
        private readonly Dictionary<IPAddress, PhysicalAddress> _networkDictionary;
        public readonly WakeOnLanCommandSettings Settings;

        public WakeOnLanSettingsForm(WakeOnLanCommandSettings settings)
        {
            InitializeComponent();            
            Settings = settings;
            cbxPcName.Text = Settings.PcName;            
            txtMacAddress.Text = Settings.MacAddress;
            _networkDictionary = GetAllDevicesOnLan();
        }

        private void cbxPcName_DropDown(object sender, EventArgs e)
        {
            if (cbxPcName.DataSource == null)
            {
                Cursor.Current = Cursors.WaitCursor;
                DirectoryEntry root = new DirectoryEntry("WinNT:");
                var pcs = new List<string>();
                foreach (DirectoryEntry computers in root.Children)
                {
                    foreach (DirectoryEntry computer in computers.Children)
                    {
                        if (computer.SchemaClassName == "Computer")
                        {
                            pcs.Add(computer.Name);
                        }
                    }
                }
                cbxPcName.DataSource = pcs;
                Cursor.Current = Cursors.Default;
            }            
        }

        private void cbxPcName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxPcName.Text))
            {
                IPHostEntry hostEntry;

                try
                {
                    hostEntry = Dns.GetHostEntry(cbxPcName.Text);
                }
                catch
                {
                    txtMacAddress.Text = string.Empty;
                    return;
                }                

                //you might get more than one ip for a hostname since 
                //DNS supports more than one record

                IPAddress[] addresses = hostEntry.AddressList.Where(a=>a.AddressFamily == AddressFamily.InterNetwork).ToArray();

                if (addresses.Length > 0)
                {                    
                    txtMacAddress.Text = BitConverter.ToString(_networkDictionary[addresses[0]].GetAddressBytes()).Replace("-",":");
                }
            }            
        }

        /// <summary>
        /// Gets the IP address of the current PC
        /// </summary>
        /// <returns></returns>
        private static IPAddress GetIPAddress()
        {
            String strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            foreach (IPAddress ip in addr)
            {
                if (!ip.IsIPv6LinkLocal)
                {
                    return ip;
                }
            }
            return addr.Length > 0 ? addr[0] : null;
        }

        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        private static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        /// <summary>
        /// MIB_IPNETROW structure returned by GetIpNetTable
        /// DO NOT MODIFY THIS STRUCTURE.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct MibIpnetrow
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwIndex;
            [MarshalAs(UnmanagedType.U4)]
            public int dwPhysAddrLen;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac0;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac1;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac2;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac3;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac4;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac5;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac6;
            [MarshalAs(UnmanagedType.U1)]
            public byte mac7;
            [MarshalAs(UnmanagedType.U4)]
            public int dwAddr;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

        /// <summary>
        /// GetIpNetTable external method
        /// </summary>
        /// <param name="pIpNetTable"></param>
        /// <param name="pdwSize"></param>
        /// <param name="bOrder"></param>
        /// <returns></returns>
        [DllImport("IpHlpApi.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        static extern int GetIpNetTable(IntPtr pIpNetTable, [MarshalAs(UnmanagedType.U4)] ref int pdwSize, bool bOrder);

        /// <summary>
        /// Get the IP and MAC addresses of all known devices on the LAN
        /// </summary>
        /// <remarks>
        /// 1) This table is not updated often - it can take some human-scale time 
        ///    to notice that a device has dropped off the network, or a new device
        ///    has connected.
        /// 2) This discards non-local devices if they are found - these are multicast
        ///    and can be discarded by IP address range.
        /// </remarks>
        /// <returns></returns>
        private static Dictionary<IPAddress, PhysicalAddress> GetAllDevicesOnLan()
        {
            Dictionary<IPAddress, PhysicalAddress> all = new Dictionary<IPAddress, PhysicalAddress>();

            // Add this PC to the list...
            all.Add(GetIPAddress(), GetMacAddress());

            int spaceForNetTable = 0;
            // Get the space needed
            // We do that by requesting the table, but not giving any space at all.
            // The return value will tell us how much we actually need.
            GetIpNetTable(IntPtr.Zero, ref spaceForNetTable, false);
            // Allocate the space
            // We use a try-finally block to ensure release.
            IntPtr rawTable = IntPtr.Zero;
            try
            {
                rawTable = Marshal.AllocCoTaskMem(spaceForNetTable);
                // Get the actual data
                int errorCode = GetIpNetTable(rawTable, ref spaceForNetTable, false);
                if (errorCode != 0)
                {
                    // Failed for some reason - can do no more here.
                    throw new Exception(string.Format(
                        "Unable to retrieve network table. Error code {0}", errorCode));
                }
                // Get the rows count
                int rowsCount = Marshal.ReadInt32(rawTable);
                IntPtr currentBuffer = new IntPtr(rawTable.ToInt64() + Marshal.SizeOf(typeof(Int32)));
                // Convert the raw table to individual entries
                MibIpnetrow[] rows = new MibIpnetrow[rowsCount];
                for (int index = 0; index < rowsCount; index++)
                {
                    rows[index] = (MibIpnetrow)Marshal.PtrToStructure(new IntPtr(currentBuffer.ToInt64() +
                                                                                  (index * Marshal.SizeOf(typeof(MibIpnetrow)))
                        ),
                        typeof(MibIpnetrow));
                }
                // Define the dummy entries list (we can discard these)
                PhysicalAddress virtualMac = new PhysicalAddress(new byte[] { 0, 0, 0, 0, 0, 0 });
                PhysicalAddress broadcastMac = new PhysicalAddress(new byte[] { 255, 255, 255, 255, 255, 255 });
                foreach (MibIpnetrow row in rows)
                {
                    IPAddress ip = new IPAddress(BitConverter.GetBytes(row.dwAddr));
                    byte[] rawMac = { row.mac0, row.mac1, row.mac2, row.mac3, row.mac4, row.mac5 };
                    PhysicalAddress pa = new PhysicalAddress(rawMac);
                    if (!pa.Equals(virtualMac) && !pa.Equals(broadcastMac) && !IsMulticast(ip))
                    {
                        //Console.WriteLine("IP: {0}\t\tMAC: {1}", ip.ToString(), pa.ToString());
                        if (!all.ContainsKey(ip))
                        {
                            all.Add(ip, pa);
                        }
                    }
                }
            }
            finally
            {
                // Release the memory.
                Marshal.FreeCoTaskMem(rawTable);
            }
            return all;
        }        

        /// <summary>
        /// Returns true if the specified IP address is a multicast address
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool IsMulticast(IPAddress ip)
        {
            bool result = true;
            if (!ip.IsIPv6Multicast)
            {
                byte highIP = ip.GetAddressBytes()[0];
                if (highIP < 224 || highIP > 239)
                {
                    result = false;
                }
            }
            return result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {                       
            if (string.IsNullOrEmpty(cbxPcName.Text))
            {
                MessageBox.Show("Please specify PC Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }
            if (string.IsNullOrEmpty(txtMacAddress.Text))
            {
                MessageBox.Show("Please specify Mac Address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(txtMacAddress.Text, "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$"))
            {
                MessageBox.Show("Mac Address format should be XX:XX:XX:XX:XX:XX.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Settings.PcName = cbxPcName.Text;            
            Settings.MacAddress = txtMacAddress.Text;            
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
