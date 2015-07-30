using System.Xml.Linq;

namespace QuranCS
{
    /// <summary>
    /// Represents a single Ayah of the Quran.
    /// </summary>
    public class Ayah
    {

        /// <summary>
        /// The index of the Surah which the Ayah belongs to.
        /// </summary>
        public int SurahNumber { get; private set; }

        /// <summary>
        /// The Surah which the Ayah belongs to.
        /// </summary>
        public Surah ParentSurah
        {
            get { return new Surah(SurahNumber); }
        }

        public int AyahNumber { get; private set; }
        /// <summary>
        /// The XML element which contains the Quranic text of the Ayah, to be used internally.
        /// </summary>
        private readonly XElement ayahElement;


        /// <summary>
        /// Instantiates a new Ayah based in the Surah and Ayah indexes provided
        /// </summary>
        /// <param name="surahNumber"> The index of the Surah wich the Ayah belongs to </param>
        /// <param name="ayahNumber"> The index of the Ayah in the Surah</param>
        public Ayah(int surahNumber, int ayahNumber)
        {
            SurahNumber = surahNumber;
            AyahNumber = ayahNumber;
            ayahElement = Quran.GetAyahElement(SurahNumber, AyahNumber);
        }

        /// <summary>
        /// Instantiates a new Ayah based in the Surah and Ayah indexes, as well as the Ayah XML element.
        /// To be used by the Quran class
        /// </summary>
        /// <param name="surahNumber"> The index of the Surah wich the Ayah belongs to </param>
        /// <param name="ayahNumber"> The index of the Ayah in the Surah</param>
        /// <param name="ayahElement"> The XML element which contains the Quranic text of the Ayah, to be used internally.</param>
        internal Ayah(int surahNumber, int ayahNumber, XElement ayahElement)
        {
            SurahNumber = surahNumber;
            AyahNumber = ayahNumber;
            this.ayahElement = ayahElement;
        }

        /// <summary>
        /// Convert the Ayah object into a string of the Quranic text it contains
        /// </summary>
        /// <returns> The Quranic text of the Ayah </returns>
        public override string ToString()
        {
            return ayahElement.Attribute("text").Value;
        }
    }
}
