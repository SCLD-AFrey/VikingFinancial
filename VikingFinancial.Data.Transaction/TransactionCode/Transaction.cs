using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace VikingFinancial.Data.Transaction
{

    public partial class Transaction
    {
        public Transaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
