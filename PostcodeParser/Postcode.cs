using System.Text.RegularExpressions;

namespace PostcodeParser
{
    public class Postcode
    {
        #region Constants
        private const string CompletePostcodePattern = "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$";
        private const string PartialPostcodePattern = "[A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]?";
        private const string AlphabeticalPattern = "^[a-zA-Z]+$";
        private const string DistrictPattern = @"(\d\d)|(\d[a-zA-Z])|(\d)";
        private static readonly char[] CharDigits = "0123456789".ToCharArray();
        #endregion

        #region Constructor
        public Postcode(string postcode)
        {
            var sanitizedPostcode = postcode.Replace(" ", "");

            this.Partial = this.PartialCheck(sanitizedPostcode);
            this.Complete = this.CompleteCheck(sanitizedPostcode);
            this.Normalized = this.NormalizePostcode(sanitizedPostcode);
        }
        #endregion

        #region Public Properties

        #region Outward Code
        /// <summary>
        /// The outward code is the part of the postcode before the single space in the middle.
        /// It is between two and four characters long.
        /// Examples of outward codes include "L1", "W1A", "RH1", "RH10" or "SE1P".
        /// </summary>
        public string OutwardCode
        {
            get
            {
                var outwardCode = Regex.Match(this.Normalized, PartialPostcodePattern).Value;

                if (this.Complete)
                {
                    var noSpaces = this.Normalized.Replace(" ", "");
                    var lastDigit = noSpaces.LastIndexOfAny(CharDigits);
                    if (lastDigit != -1)
                    {
                        outwardCode = noSpaces.Substring(0, lastDigit);
                    }
                }

                return outwardCode;
            }
        }
        /// <summary>
        /// The postcode area is part of the outward code.
        /// The postcode area is either one or two characters long and is all letters.
        /// Examples of postcode areas include "L" for Liverpool, "RH" for Redhill and "EH" for Edinburgh.
        /// A postal area may cover a wide area, for example "RH" covers north Sussex, and "BT" (Belfast) covers the whole of Northern Ireland.
        /// </summary>
        public string Area
        {
            get
            {
                var area = string.Empty;
                if (!Partial)
                    return area;

                var twoLetterArea = this.Normalized.Replace(" ", "").Substring(0, 2);
                if (Regex.IsMatch(twoLetterArea, AlphabeticalPattern))
                {
                    area = twoLetterArea;
                }

                if (string.IsNullOrEmpty(area))
                {
                    var oneLetterArea = this.Normalized.Substring(0, 1);
                    if (Regex.IsMatch(oneLetterArea, AlphabeticalPattern))
                    {
                        area = oneLetterArea;
                    }
                }

                return area;
            }
        }
        /// <summary>
        /// The postcode district is made of one or two digits or a digit followed by a letter.
        /// The outward code is between two and four characters long. Examples include "W1A", "RH1", "RH10" or "SE1P".
        /// </summary>
        public string District
        {
            get
            {
                var district = string.Empty;
                if (!Partial)
                    return district;

                district = this.OutwardCode.Substring(this.Area.Length);
                if (!Regex.IsMatch(district, DistrictPattern))
                {
                    district = string.Empty;
                }

                return district;
            }
        }
        #endregion

        #region Inward Code
        /// <summary>
        /// The inward code is the part of the postcode after the single space in the middle.
        /// It is three characters long. The inward code assists in the delivery of post within a postal district.
        /// Examples of inward codes include "0NY", "7GZ", "7HF", or "8JQ"
        /// </summary>
        public string InwardCode
        {
            get
            {
                var inwardCode = string.Empty;

                if (this.Complete)
                {
                    var noSpaces = this.Normalized.Replace(" ", "");
                    var lastDigit = noSpaces.LastIndexOfAny(CharDigits);
                    if (lastDigit != -1)
                    {
                        inwardCode = noSpaces.Substring(lastDigit);
                    }
                }

                return inwardCode;
            }
        }

        /// <summary>
        /// The postcode sector is made up of the postcode district, the single space, and the first character of the inward code.
        /// It is between four and six characters long (including the single space).
        /// </summary>
        public string Sector
        {
            get
            {
                var sector = string.Empty;
                if (!string.IsNullOrEmpty(this.OutwardCode) && !string.IsNullOrEmpty(this.InwardCode))
                {
                    sector = $"{this.OutwardCode} {this.InwardCodeFirstCharacter}";
                }
                return sector;
            }
        }

        /// <summary>
        /// The postcode unit is two characters added to the end of the postcode sector. 
        /// </summary>
        public string Unit => (!string.IsNullOrEmpty(this.InwardCode)) ? InwardCode.Substring(1) : string.Empty;
        #endregion

        public bool IsValid => (this.Partial || this.Complete);
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return this.Normalized;
        }
        #endregion

        #region Private Properties
        private string InwardCodeFirstCharacter => (!string.IsNullOrEmpty(this.InwardCode)) ? InwardCode.Substring(0, 1) : string.Empty;
        private string Normalized { get; set; }
        private bool Partial { get; set; }
        private bool Complete { get; set; }
        #endregion

        #region Private Helper Methods
        private bool PartialCheck(string postcode) => Regex.IsMatch(postcode, PartialPostcodePattern);
        private bool CompleteCheck(string postcode) => Regex.IsMatch(postcode, CompletePostcodePattern);
        private string NormalizePostcode(string postcode)
        {
            var normalized = string.Empty;

            if (this.Complete)
            {
                var noSpaces = postcode.Replace(" ", "");
                var lastDigit = noSpaces.LastIndexOfAny(CharDigits);
                if (lastDigit != -1)
                {
                    normalized = noSpaces.Insert(lastDigit, " ").ToUpper();
                }
            }
            else if (this.Partial)
            {
                normalized = Regex.Match(postcode, PartialPostcodePattern).Value.ToUpper();
            }

            return normalized;
        }
        #endregion
    }
}