﻿// Copyright 2016 Google Inc. All Rights Reserved.
// Licensed under the Apache License Version 2.0.

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Services; 
using Google.Apis.Dns.v1;
using Google.Apis.Dns.v1.Data;
using Google.PowerShell.Common;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Google.PowerShell.Dns
{
    /// <summary>
    /// <para type="synopsis">
    /// Gets the Google DNS ManagedZones that have been created but not yet deleted within a project.
    /// </para>
    /// <para type="description">
    /// Lists the project's ManagedZones.
    /// </para>
    /// <para type="description">
    /// If a project is specified, will instead return all ManagedZones governed by that project. 
    /// The filter ManagedZone can be provided to return that specific zone.
    /// </para>
    /// <example>
    ///   <para>Get the managed zones for the project "testing."</para>
    ///   <para><code>Get-GcdManagedZones -Project "testing" </code></para>
    /// </example>
    /// /// <example>
    ///   <para>Get the managed zone "test1" for the project "testing."</para>
    ///   <para><code>Get-GcdManagedZones -Project "testing" -ManagedZone "test1" </code></para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "GcdManagedZone")]
    public class GetGcdManagedZoneCmdlet : GcdCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// The project to check for managed zones.
        /// </para>
        /// </summary>
        [Parameter]
        [ConfigPropertyName(CloudSdkSettings.CommonProperties.Project)]
        public string Project { get; set; }

        /// <summary>
        /// <para type="description">
        /// Specific ManagedZone to return (name or id permitted).
        /// </para>
        [Parameter(Position = 1, Mandatory = false)]
        public string ManagedZone { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (!String.IsNullOrEmpty(Project))
            {
                if (!String.IsNullOrEmpty(ManagedZone))
                {
                   ManagedZonesResource.GetRequest zoneGetRequest = Service.ManagedZones.Get(Project, ManagedZone);
                   ManagedZone zone = zoneGetRequest.Execute();
                   WriteObject(zone);
                }
                else
                {
                    ManagedZonesResource.ListRequest zonesListRequest = Service.ManagedZones.List(Project);
                    IList<ManagedZone> zonesList = zonesListRequest.Execute().ManagedZones;
                    WriteObject(zonesList, true);
                }
            }
        }
    }
}
