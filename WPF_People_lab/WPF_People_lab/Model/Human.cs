//-----------------------------------------------------------------------
// <copyright file="Human.cs" company="My Company">
//     Some info
// </copyright>
//-----------------------------------------------------------------------

namespace WPF_People_lab.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;
    using Infrastructure;

    [Serializable]

    /// <summary>
    /// Class Human represents human's essence
    /// </summary>
    public class Human : ICloneable
    {
        [XmlAttribute]

        /// <summary>
        /// This field contain human's name
        /// </summary>
        private string name;

        [XmlAttribute]

        /// <summary>
        /// This field contain human's age
        /// </summary>
        private int age;

        [XmlAttribute]

        /// <summary>
        /// This field contain human's weight
        /// </summary>
        private float weight;

        [XmlAttribute]

        /// <summary>
        /// Is the human alive or not.
        /// If human is alive this field has true value
        /// If human isn't alive this field has false value
        /// </summary>
        private bool isAlive;

        /// <summary>
        /// Initializes a new instance of the <see cref="Human"/> class.
        /// </summary>
        public Human()
        {
            this.name = "Ben";
            this.age = 20;
            this.weight = 50f;
            this.isAlive = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Human"/> class.
        /// </summary>
        /// <param name="newName">string type for initializing name's field</param>
        /// <param name="newAge">integer type for initializing age's field</param>
        /// <param name="newWeigth">float type for initializing weight's field</param>
        public Human(string newName = "Ben", int newAge = 20, float newWeigth = 50f)
        {
            this.name = newName;
            this.age = newAge;
            this.weight = newWeigth;
            this.isAlive = true;
        }

        /// <summary>
        /// Event which handel human's state changing
        /// </summary>
        public event Action StateChanged;

        /// <summary>
        /// Gets or sets values of the name's field 
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    string newName = value;
                    this.name = newName;
                    if (this.StateChanged != null)
                    {
                        this.StateChanged();
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Inccorect argument of name, please try again! The sender is {0}", e);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Gets or sets age's field
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                try
                {
                    this.age = value;
                    if (this.StateChanged != null)
                    {
                        this.StateChanged();
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Inccorect argument of age, please try again! The sender is {0}", e);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Gets or sets weight's field
        /// </summary>
        public float Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                try
                {
                    this.weight = value;
                    if (this.StateChanged != null)
                    {
                        this.StateChanged();
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Inccorect argument of weight, please try again! The sender is {0}", e);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether human is alive
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return this.isAlive;
            }

            set
            {
                this.isAlive = value;
            }
        }

        /// <summary>
        /// This method overloads ToString method 
        /// and returns information about human's name, age, weight
        /// and information about human's state (alive him or not)
        /// </summary>
        /// <returns>string type returning</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(string.Format("***\nHuman's name is {0}.\n{0} is {1} years old.\n{0} is {2} kg of weight!\n", this.name, this.age, this.weight));
            str.Append(string.Format("Human state is {0}.\n***\n\n", this.isAlive == true ? "alive" : "dead"));
            return str.ToString();
        }

        /// <summary>
        /// Clone the Human object
        /// </summary>
        /// <returns>new object with the same fields</returns>
        public object Clone()
        {
            return new Human(this.name, this.age, this.weight);
        }

        /// <summary>
        /// There human grows up until he won't dead
        /// </summary>
        /// <param name="deltaYears">count of years to travel</param>
        public void Grow(int deltaYears)
        {
            if (this.isAlive)
            {
                if (deltaYears <= 0)
                {
                    throw new ArgumentOutOfRangeException("Sorry, can't grow back!");
                }
                else
                {
                    this.age += deltaYears;
                    if (this.age > 90)
                    {
                        this.isAlive = false;
                    }
                    else
                    {
                        Console.WriteLine("{0} has just grown up.{0} is {1} years old!", this.name, this.age);
                    }
                } 
            }
        }
    }
}
