using Demo.StartWars.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Demo.AspNetCore.JqGrid.Model
{
    public class DictionariesViewModel
    {
        #region Fields
        private static readonly IDictionary<string, string> _gendersDictionary;
        private static readonly IDictionary<string, string> _skinColorsDictionary;
        private static readonly IDictionary<string, string> _hairColorsDictionary;
        private static readonly IDictionary<string, string> _eyeColorsDictionary;
        #endregion

        #region Constructor
        static DictionariesViewModel()
        {
            _gendersDictionary = new Dictionary<string, string>();
            foreach (Genders gender in Enum.GetValues(typeof(Genders)))
            {
                _gendersDictionary.Add(((int)gender).ToString(CultureInfo.InvariantCulture), gender.ToString());
            }

            _skinColorsDictionary = new Dictionary<string, string>();
            foreach (SkinColors skinColor in Enum.GetValues(typeof(SkinColors)))
            {
                _skinColorsDictionary.Add(((int)skinColor).ToString(CultureInfo.InvariantCulture), skinColor.ToString());
            }

            _hairColorsDictionary = new Dictionary<string, string>();
            foreach (HairColors hairColor in Enum.GetValues(typeof(HairColors)))
            {
                _hairColorsDictionary.Add(((int)hairColor).ToString(CultureInfo.InvariantCulture), hairColor.ToString());
            }

            _eyeColorsDictionary = new Dictionary<string, string>();
            foreach (EyeColors eyeColor in Enum.GetValues(typeof(EyeColors)))
            {
                _eyeColorsDictionary.Add(((int)eyeColor).ToString(CultureInfo.InvariantCulture), eyeColor.ToString());
            }
        }
        #endregion

        #region Methods
        public IDictionary<string, string> GetGendersDictionary()
        {
            return _gendersDictionary;
        }

        public IDictionary<string, string> GetSkinColorsDictionary()
        {
            return _skinColorsDictionary;
        }

        public IDictionary<string, string> GetHairColorsDictionary()
        {
            return _hairColorsDictionary;
        }

        public IDictionary<string, string> GetEyeColorsDictionary()
        {
            return _eyeColorsDictionary;
        }
        #endregion
    }
}
