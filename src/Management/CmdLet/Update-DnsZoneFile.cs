﻿using System;
using System.Management;
using System.Management.Automation;
using DnsShell.Management;

namespace DnsShell.PowerShell.CmdLet
{
    [Cmdlet(VerbsData.Update, "DnsZoneFile")]
    public class UpdateDnsZoneFile : ManagementCmdlet
    {
        #region Parameter:Identity (String) :: Parameter Set <All>
        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Identity",
            ParameterSetName = "")]
        [ValidatePattern(@"^\\\\.*\\root\\MicrosoftDNS:MicrosoftDNS_")]
        public String Identity;
        #endregion

        #region Parameter:Server (String) :: Parameter Set <All>
        /// <summary>
        /// Specify the ServerName to operate on. Defaults to localhost.
        /// </summary>
        [Parameter(
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server",
            ParameterSetName = "")]
        [Alias("ComputerName", "ServerName")]
        public String Server = "localhost";
        #endregion

        protected override void ProcessRecord()
        {
            this.SetOptions();
            this.SetScope(Server);
            this.SetManagementPath(Identity);
            ManagementObject wmiZone = this.Get();

            Zone DnsZone = new Zone(wmiZone, Server);

            DnsZone.WriteBackZone();
        }
    }
}
