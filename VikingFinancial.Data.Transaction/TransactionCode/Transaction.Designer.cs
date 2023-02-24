﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace VikingFinancial.Data.Transaction
{

    public partial class Transaction : XPObject
    {
        double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>(nameof(Amount), ref fAmount, value); }
        }
        string fDesc;
        public string Desc
        {
            get { return fDesc; }
            set { SetPropertyValue<string>(nameof(Desc), ref fDesc, value); }
        }
        string fNote;
        [Size(SizeAttribute.Unlimited)]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>(nameof(Note), ref fNote, value); }
        }
        DateTime fDateCreated;
        public DateTime DateCreated
        {
            get { return fDateCreated; }
            set { SetPropertyValue<DateTime>(nameof(DateCreated), ref fDateCreated, value); }
        }
        DateTime fDateTransaction;
        public DateTime DateTransaction
        {
            get { return fDateTransaction; }
            set { SetPropertyValue<DateTime>(nameof(DateTransaction), ref fDateTransaction, value); }
        }
        UserProfile fAddedByUser;
        [Association(@"TransactionReferencesUserProfile")]
        public UserProfile AddedByUser
        {
            get { return fAddedByUser; }
            set { SetPropertyValue<UserProfile>(nameof(AddedByUser), ref fAddedByUser, value); }
        }
    }

}