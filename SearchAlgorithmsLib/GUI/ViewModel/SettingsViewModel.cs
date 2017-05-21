﻿using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel

{
    class SettingsViewModel : ViewModel {

        private ISettingsModel model;

        public SettingsViewModel()
        {
            this.model = new AppSettingsModel();
        }
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        public int ServerPort
        {
            get {
                return model.ServerPort;
            }
            set {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public int MazeRows {
            get {
                return model.MazeRows;
            }
            set {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols {
            get {
                return model.MazeCols;
            }
            set {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int SearchAlgorithm {
            get {
                return model.SearchAlgorithm;
            }
            set {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
