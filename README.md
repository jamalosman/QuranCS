# QuranCS

A C# Portable Class Library to access the Qur'an

This library draws its data from the Tanzil project: http://tanzil.net/wiki/

The project has two XML files from Tanzil, quran-uthmani.xml (the actual Quranic text in XML) and quran-data.xml (metadata),
both resoruces are built into the project as embedded resources and so this API should be usable in WPF, Windows 8+ Apps, 
Windows Phone 8+ apps, Xamarin apps etc. 

Currently still a work in progress

# Current Features

 - Access any Surah: simply pass the Surah number to the Surah constructor e.g.
 
 <code>
 var fatihah = new Surah(1);
 </code>
 
 The Surah object allows you to access different aspects of the Surah such as Name, RomanizedName etc.
 
 - Access any Ayah: simply pass the Surah and Ayah number to the Ayah constructor e.g.
 
 <code>
 var naas1 = new Ayah(114,1);
 </code>
 
 -
 
 More features will be added regularly, Allah willing,
 
 Project is open to suggesstions.
