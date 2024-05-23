﻿#pragma checksum "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3077D6F6FE6F3206928C5C242932AEB9DE8F62D95082080E3DF4EF89774955AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Warehouse.View.DeliveryNote;


namespace Warehouse.View.DeliveryNote {
    
    
    /// <summary>
    /// CreateDelivery
    /// </summary>
    public partial class CreateDelivery : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DivisionComboBox;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePicker;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AssignmentBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ContractorGrid;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem AddContractor;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem EditContractor;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DeleteContractor;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Confirm;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Return;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Warehouse;component/view/deliverynote/createdelivery.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DivisionComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.DatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.AssignmentBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ContractorGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.AddContractor = ((System.Windows.Controls.MenuItem)(target));
            
            #line 124 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            this.AddContractor.Click += new System.Windows.RoutedEventHandler(this.AddContractor_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.EditContractor = ((System.Windows.Controls.MenuItem)(target));
            
            #line 125 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            this.EditContractor.Click += new System.Windows.RoutedEventHandler(this.EditContractor_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeleteContractor = ((System.Windows.Controls.MenuItem)(target));
            
            #line 126 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            this.DeleteContractor.Click += new System.Windows.RoutedEventHandler(this.DeleteContractor_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            this.Confirm.Click += new System.Windows.RoutedEventHandler(this.Confirm_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Return = ((System.Windows.Controls.Button)(target));
            
            #line 167 "..\..\..\..\View\DeliveryNote\CreateDelivery.xaml"
            this.Return.Click += new System.Windows.RoutedEventHandler(this.Return_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

