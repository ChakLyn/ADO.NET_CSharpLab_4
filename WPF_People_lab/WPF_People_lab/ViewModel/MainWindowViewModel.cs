//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="My Company">
//     Some info
// </copyright>
//-----------------------------------------------------------------------

namespace WPF_People_lab.ViewModel
{    
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using Infrastructure;
    using Microsoft.Win32;
    using Model;

    /// <summary>
    /// Class for main window view
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Human object that mast be added to the collection
        /// </summary>
        private Human currentHuman;

        /// <summary>
        /// Temp collection of people 
        /// </summary>
        private ObservableCollection<Human> people;
        
        /// <summary>
        /// Command for saving collection
        /// </summary>
        private Command savePeopleCommand;
        
        /// <summary>
        /// Command for loading collection of people 
        /// </summary>
        private Command loadPeopleCommand;

        /// <summary>
        /// Command for adding some people into collection
        /// </summary>
        private Command addHumanCommandCommand;

        /// <summary>
        /// Command that make all people in collection grow up
        /// </summary>
        private Command travelInFutureCommand;

        /// <summary>
        /// Command that exit from application
        /// </summary>
        private Command exitCommand;

        /// <summary>
        /// Gets or sets current human
        /// </summary>
        public Human CurrentHuman
        {
            get
            {
                if (this.currentHuman == null)
                {
                    this.currentHuman = new Human();
                }

                return this.currentHuman;
            }

            set
            {
                this.currentHuman = value;
                this.OnPropertyChanged("CurrentHuman");
            }
        }

        /// <summary>
        /// Gets people collection
        /// </summary>
        public ObservableCollection<Human> People
        {
            get
            {
                this.people = PeopleContainer.AllPeople;
                return this.people;
            }
        }

        /// <summary>
        /// Gets command for growing up 
        /// </summary>
        public ICommand TravelInFuture
        {
            get
            {
                if (this.travelInFutureCommand == null)
                {
                    this.travelInFutureCommand = new Command(
                        this.ExecutedTravelInFutureCommand,
                        this.CanExecutedTravelInFutureCommand);
                }

                return this.travelInFutureCommand;
            }
        }

        /// <summary>
        /// Gets command for saving
        /// </summary>
        public ICommand SavePeople
        {
            get
            {
                if (this.savePeopleCommand == null)
                {
                    this.savePeopleCommand = new Command(
                        this.ExecutedSavePeopleCommand,
                        this.CanExecuteSavePeopleCommand);
                }

                return this.savePeopleCommand;
            }
        }

        /// <summary>
        /// Gets command for loading
        /// </summary>
        public ICommand LoadPeople
        {
            get
            {
                if (this.loadPeopleCommand == null)
                {
                    this.loadPeopleCommand = new Command(this.ExecutedLoadPeopleCommand);
                }

                return this.loadPeopleCommand;
            }
        }

        /// <summary>
        /// Gets for adding
        /// </summary>
        public ICommand AddHuman
        {
            get
            {
                if (this.addHumanCommandCommand == null)
                {
                    this.addHumanCommandCommand = new Command(
                        this.ExecutedAddHumanCommand,
                        this.CanExecuteAddHumanCommand);
                }

                return this.addHumanCommandCommand;
            }
        }

        /// <summary>
        /// Gets for exit
        /// </summary>
        public ICommand Exit
        {
            get
            {
                if (this.exitCommand == null)
                {
                    this.exitCommand = new Command(
                        this.ExecuteExitCommand);
                }

                return this.exitCommand;
            }
        }

        /// <summary>
        /// Clear people's collection
        /// </summary>
        protected override void OnDispose()
        {
            this.People.Clear();
        }

        /// <summary>
        /// What will be executed for exit command
        /// </summary>
        /// <param name="parametr">some objects parameter</param>
        private void ExecuteExitCommand(object parametr)
        {
            if (!PeopleContainer.IsDataSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "There are some new data. Do you wanna to save them?",
                    "Confirm",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.ExecutedSavePeopleCommand(0);
                }
            }

            Application.Current.Shutdown();
        }

        /// <summary>
        /// What will be as predicate for travel in future command
        /// </summary>
        /// <param name="parametr">Some parameter</param>
        /// <returns>can execute</returns>
        private bool CanExecutedTravelInFutureCommand(object parametr)
        {
            if (this.people.Count != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// What will be executed for travel in future command
        /// </summary>
        /// <param name="parametr">some objects parameter</param>
        private void ExecutedTravelInFutureCommand(object parametr)
        {
            PeopleContainer.Future(5);
            this.OnPropertyChanged("People");
        }

        /// <summary>
        /// What will be as predicate for save command
        /// </summary>
        /// <param name="parametr">Some parameter</param>
        /// <returns>can execute</returns>
        private bool CanExecuteSavePeopleCommand(object parametr)
        {
            if (!PeopleContainer.IsDataSaved)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// What will be executed for save command
        /// </summary>
        /// <param name="parametr">some objects parameter</param>
        private void ExecutedSavePeopleCommand(object parametr)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "(*.xml)|*.xml";
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                PeopleContainer.Save(dialog.FileName);
            }
        }

        /// <summary>
        /// What will be executed for load command
        /// </summary>
        /// <param name="parametr">some objects parameter</param>
        private void ExecutedLoadPeopleCommand(object parametr)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.xml)|*.xml";
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                PeopleContainer.Load(dialog.FileName);
            }

            this.OnPropertyChanged("People");
        }

        /// <summary>
        /// What will be executed for add command
        /// </summary>
        /// <param name="parameter">some objects parameter</param>
        private void ExecutedAddHumanCommand(object parameter)
        {
            PeopleContainer.AllPeople.Add(this.CurrentHuman);
            this.OnPropertyChanged("People");
            this.CurrentHuman = null;
        }

        /// <summary>
        /// What will be as predicate for add command
        /// </summary>
        /// <param name="parameter">Some parameter</param>
        /// <returns>can execute</returns>
        private bool CanExecuteAddHumanCommand(object parameter)
        {
            if (string.IsNullOrEmpty(this.CurrentHuman.Name) ||
                string.IsNullOrEmpty(Convert.ToString(this.CurrentHuman.Weight)) ||
                string.IsNullOrEmpty(Convert.ToString(this.CurrentHuman.Weight)))
            {
                return false;
            }

            return true;
        }
    }
}
