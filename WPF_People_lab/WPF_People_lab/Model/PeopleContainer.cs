//-----------------------------------------------------------------------
// <copyright file="PeopleContainer.cs" company="My Company">
//     Some info
// </copyright>
//-----------------------------------------------------------------------

namespace WPF_People_lab.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Container for human object
    /// </summary>
    public static class PeopleContainer
    {
        /// <summary>
        /// Observable collection for people
        /// </summary>
        private static ObservableCollection<Human> people;        

        /// <summary>
        /// True if data is saved
        /// False in opposite way
        /// </summary>
        private static bool dataSaved = true;

        /// <summary>
        /// Gets or sets a value indicating whether saved state
        /// </summary>
        public static bool IsDataSaved
        {
            get
            {
                return dataSaved == true;
            }

            set
            {
                dataSaved = value;
            }
        }

        /// <summary>
        /// Gets people collection
        /// </summary>
        public static ObservableCollection<Human> AllPeople
        {
            get
            {
                if (people == null)
                {
                    CreatePeople();
                }

                foreach (Human human in people)
                {
                    human.StateChanged += delegate { dataSaved = false; };
                }

                return people;
            }
        }

        /// <summary>
        /// Travel in the future increasing the age of humans
        /// </summary>
        /// <param name="detlaYears">How much years to go</param>
        public static void Future(int detlaYears)
        {
            if (detlaYears > 0)
            {
                ObservableCollection<Human> temp = new ObservableCollection<Human>(people);
                foreach (Human human in temp)
                {
                    human.Grow(detlaYears);
                }

                people = temp;
                PeopleContainer.dataSaved = false;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Can't grow back!");
            }
        }
        
        /// <summary>
        /// Save list of humans in .xml file
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public static void Save(string fileName)
        {
            dataSaved = true;
            try
            {
                using (Stream fileStream = new FileStream(
                    fileName,
                   FileMode.Create,
                   FileAccess.Write,
                   FileShare.None))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Human>));
                    xmlFormat.Serialize(fileStream, people);
                    fileStream.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Something wrong with file! Try again\n{0}", e);
            }
        }

        /// <summary>
        /// Load list of human into container from .xml file
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public static void Load(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Human>));
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(fs);
                    people = (ObservableCollection<Human>)serializer.Deserialize(reader);
                    people.CollectionChanged += delegate { dataSaved = false; };
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Something wrong with deserializisation {0}", e);
            }
        }

        /// <summary>
        /// Creates new collection
        /// </summary>
        private static void CreatePeople()
        {
            people = new ObservableCollection<Human>();
            people.CollectionChanged += delegate { dataSaved = false; };
        }                
    }
}
