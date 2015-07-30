using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace QuranCS
{
    /// <summary>
    /// Provides methods for interacting with the Quranic XML document
    /// </summary>
    public static class Quran
    {

        private static readonly XDocument QuranDoc = GetXmlResourceFile("quran-uthmani");
        private static readonly XDocument QuranMetaDataDoc = GetXmlResourceFile("quran-data");

        /// <summary>
        /// Accesses embedded XML resource files
        /// </summary>
        /// <param name="filename"> The name of the XML file to be accessed (without the file extension)</param>
        /// <returns> The XML document as a Linq Object </returns>
        private static XDocument GetXmlResourceFile(string filename)
        {
            var stream = typeof(Quran).GetTypeInfo().Assembly.GetManifestResourceStream("CSQuranPortable."+filename+".xml");
            return XDocument.Load(stream);       
        }

        /// <summary>
        /// Gets all the names of the Surahs in the Quran transcribed into Latin letters
        /// </summary>
        /// <returns> An IEnumerable that iterates over the names </returns>
        public static IEnumerable<string> GetSurahRomanizedNames()
        {
            return QuranMetaDataDoc
                .Element("quran")
                .Element("suras")
                .Elements()
                .Select(node => node.Attribute("index").Value + ": Surah" + node.Attribute("tname").Value);
        }


        /// <summary>
        /// Gets all the names of the Surahs in the Quran in Arabic letters
        /// </summary>
        /// <returns> An IEnumerable that iterates over the names </returns>
        public static IEnumerable<string> GetSurahNames()
        {
            return QuranMetaDataDoc
                .Element("quran")
                .Element("suras")
                .Elements()
                .Select(node => node.Attribute("index").Value + ": Surah" + node.Attribute("name").Value);
        }

        /// <summary>
        /// Extracts the element for a single "sura" elememt from the Quran XML Document
        /// </summary>
        /// <param name="surahNumber"> The index of "sura" element to extract </param>
        /// <returns> The "sura" element </returns>
        internal static XElement GetSurahElement(int surahNumber)
        {
            var xElement = QuranDoc
                .Element("quran");
            if (xElement != null)
            {
                return xElement
                    .Elements()
                    .First(s => s.Attribute("index").Value == surahNumber.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts the metadata "sura" elememt from the Quranic Metadata XML Document
        /// </summary>
        /// <param name="surahNumber"> The index of "sura" element to extract </param>
        /// <returns> The "sura" metadata element </returns>
        internal static XElement GetSurahDataElement(int surahNumber)
        {
            var xElement = QuranMetaDataDoc
                .Element("quran");
            if (xElement != null)
            {
                return xElement
                    .Element("suras")
                    .Elements()
                    .First(s => s.Attribute("index").Value == surahNumber.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts the "aya" element from the Quranic XML Document
        /// </summary>
        /// <param name="surahNumber">The index of "sura" element to which the "ayah" element belongs</param>
        /// <param name="ayahNumber"> The index of "ayah" element to extract</param>
        /// <returns>the "ayah" element </returns>
        internal static XElement GetAyahElement(int surahNumber, int ayahNumber)
        {
            var qElement = QuranDoc
                .Element("quran");
            if (qElement != null)
            {
                return qElement
                    .Elements()
                    .First(surah => surah.Attribute("index").Value == surahNumber.ToString())
                    .Elements()
                    .First(ayah => ayah.Attribute("index").Value == ayahNumber.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Generates Surah objects for every Surah in the Quran
        /// </summary>
        /// <returns>An IEnumerator that iterates over all the Surahs in the Quran</returns>
        public static IEnumerator<Surah> GetSurahEnumerator()
        {
            // Get the Enumerator for al the sura Elements in the Quran Document
            var sElements = QuranDoc
                .Element("quran")
                .Elements().GetEnumerator();
            // Get the Enumerator for all the sura Elements in the Quran Meta Data Document
            var sDatas = QuranMetaDataDoc
                .Element("quran")
                .Element("suras")
                .Elements().GetEnumerator();

            // Loop through both Enumerators, returning the sura generated from each
            for (int i = 1; i <= 114; i++)
            {
                yield return new Surah(i, sElements.Current, sDatas.Current);
                sElements.MoveNext();
                sDatas.MoveNext();
            }


        }
    }
}

