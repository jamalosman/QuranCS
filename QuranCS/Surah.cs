using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace QuranCS
{
    /// <summary>
    ///  Represents a single Surah of the Quran
    /// </summary>
    public class Surah : IEnumerable<Ayah>
    {
        private readonly XElement _surahElement;
        private readonly XElement _surahData;

        /// <summary>
        /// The index of the Surah
        /// </summary>
        public int SurahNumber { get; private set; }

        /// <summary>
        /// The Arabic name of the surah, (using Arabic letters)
        /// </summary>
        public string Name {  get { return _surahData.Attribute("name").Value; } }

        /// <summary>
        /// The Romanized name of the Surah, (using Latin letters)
        /// </summary>
        public string RomanizedName {  get { return _surahData.Attribute("tname").Value; } }


        /// <summary>
        /// The name of the Surah translated into English
        /// </summary>
        public string TranslatedName { get { return _surahData.Attribute("ename").Value; } }
        
        /// <summary>
        /// The number of Ayahs in the Surah
        /// </summary>
        public int AyahCount
        {
            get { return int.Parse(_surahData.Attribute("ayas").Value); }
        }

        /// <summary>
        /// True if the Surah is Makki, False if it is Madani (According to the Tanzil Document).
        /// </summary>
        public bool isMakki
        {
            get { return _surahData.Attribute("type").Value == "Meccan"; }
        }


        //Constructors

        /// <summary>
        /// Creates a Surah object by searching the Quran xml document for the "sura" element with the give index
        /// </summary>
        /// <param name="surahNumber">the index of the Surah that is being searched for</param>
        public Surah(int surahNumber)
        {
            SurahNumber = surahNumber;
            _surahElement = Quran.GetSurahElement(SurahNumber);
            _surahData = Quran.GetSurahDataElement(SurahNumber);
        }

        /// <summary>
        /// Creates a Surah object from the parameters, to be used by the Quran class
        /// </summary>
        /// <param name="surahNumber"> The index of the Surah</param>
        /// <param name="surahElement">the XML "sura" element that contains the Quranic text</param>
        /// <param name="surahData">the XML metadata for this sura</param>
        internal Surah(int surahNumber, XElement surahElement, XElement surahData)
        {
            SurahNumber = surahNumber;
            _surahElement = surahElement;
            _surahData = surahData;
        }


        /// <summary>
        /// Finds an Ayah within the Surah by its index
        /// </summary>
        /// <param name="ayahNumber"></param>
        /// <returns>Returns the Ayah within the Surah with the specified index</returns>
        public Ayah GetAyah(int ayahNumber)
        {
            var ayahElement = _surahElement
                .Elements()
                .First(ayah => ayah.Attribute("index").Value == ayahNumber.ToString());
            return new Ayah(SurahNumber, ayahNumber, ayahElement);
        }


        /// <summary>
        /// Gets the Enumerator for the Ayahs in the surah
        /// </summary>
        /// <returns>the Enumerator for the Ayahs in the surah</returns>
        public IEnumerator<Ayah> GetEnumerator()
        {
            var i = 1;
            foreach (var ayahElement in _surahElement.Elements())
            {
                yield return new Ayah(SurahNumber, i, ayahElement);
                i++;
            }
        }


        /// <summary>
        /// Gets the Enumerator for the Ayahs in the surah (non-generic)
        /// </summary>
        /// <returns>the Enumerator for the Ayahs in the surah</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
