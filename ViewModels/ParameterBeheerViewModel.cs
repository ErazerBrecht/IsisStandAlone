﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class ParameterBeheerViewModel: BeheerViewModel
    {
        public Parameters  ParameterData { get; set; }
        public WinkelBeheerViewModel WinkelData { get; private set; }
        public SchoolBeheerViewModel SchoolData { get; private set; }
        public BedrijfBeheerViewModel BedrijfData { get; private set; }
        public OphalingBeheerViewModel OphalingData { get; private set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public ParameterBeheerViewModel()
        {
            Header = "ParameterBeheer";
            ParameterData = new Parameters();
            LoadParameters();

            WinkelData = new WinkelBeheerViewModel();
            SchoolData = new SchoolBeheerViewModel();
            BedrijfData = new BedrijfBeheerViewModel();
            OphalingData = new OphalingBeheerViewModel();
        }

        public void LoadParameters()
        {
            ParameterData.ParameterHemden = Properties.Settings.Default.ParameterHemden;
            ParameterData.ParameterLakens1 = Properties.Settings.Default.ParameterLakens1;
            ParameterData.ParameterLakens2 = Properties.Settings.Default.ParameterLakens2;
            ParameterData.ParameterAndereStrijk = Properties.Settings.Default.ParameterAndereStrijk;
        }

        public void SaveParameters()
        {
            Properties.Settings.Default.ParameterHemden = ParameterData.ParameterHemden;
            Properties.Settings.Default.ParameterLakens1 = ParameterData.ParameterLakens1;
            Properties.Settings.Default.ParameterLakens2 = ParameterData.ParameterLakens2;
            Properties.Settings.Default.ParameterAndereStrijk = ParameterData.ParameterAndereStrijk;
            Properties.Settings.Default.Save();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            LoadParameters();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            SaveParameters();
        }

        public override bool Close()
        {
            return false;
        }
    }
}
