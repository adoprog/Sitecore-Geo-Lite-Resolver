/**
 * LiteCountryCondition.cs
 *
 * Copyright (C) 2011 Alexander Doroshenko
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

namespace Sitecore.SharedSource.GeoLiteResolver.Conditions
{
    using System.Web;

    using Sitecore.Analytics.Data.DataAccess.DataSets;
    using Sitecore.Analytics.Rules.Conditions;
    using Sitecore.Rules;
    using Sitecore.SharedSource.GeoLiteResolver.MaxMind;

    public class LiteCountryCondition<T> : CountryCondition<T> where T : RuleContext
    {
        private static readonly LookupService lookupService =
            new LookupService(
                HttpContext.Current.Server.MapPath(
                    Sitecore.Configuration.Settings.GetSetting("Sitecore.SharedSource.GeoLiteResolver.DataFile")),
                LookupService.GEOIP_MEMORY_CACHE);

        protected override string GetColumnValue(VisitorDataSet.VisitsRow visit)
        {
            Country country = lookupService.getCountry(HttpContext.Current.Request.UserHostAddress);
            var countryCode = country.getCode();

            if(string.IsNullOrEmpty(countryCode) || countryCode == "--")
            {
                countryCode = base.GetColumnValue(visit);
            }

            return countryCode;
        }
    }
}