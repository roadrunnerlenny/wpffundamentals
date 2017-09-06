using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfFundamentals.Controls
{
    public static class SelectionComboBindingManager
    {
        #region DisplayMemberPath

        public static string GetDisplayMemberPath(DependencyObject obj)
        {
            return (string)obj.GetValue(DisplayMemberPathProperty);
        }

        public static void SetDisplayMemberPath(DependencyObject obj, string value)
        {
            obj.SetValue(DisplayMemberPathProperty, value);
        }

        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.RegisterAttached(
            "DisplayMemberPath", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetBindingsForContentControl)));

        private static void SetBindingsForContentControl(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            ContentControl contentControl = dep as ContentControl;
            DataTemplate template = GetItemTemplate(dep);
            object content = GetContent(dep);
            string displayMemberPath = GetDisplayMemberPath(dep);

            if (contentControl != null && content != null && (template != null || displayMemberPath != null))
            {
                if (template != null)
                    contentControl.ContentTemplate = template;
                else if (displayMemberPath != null)
                    contentControl.ContentTemplate = CreateDefaultTextBlockBinding(displayMemberPath);
                contentControl.Content = content;
            }
        }

        private static DataTemplate CreateDefaultTextBlockBinding(string displayMemberPath)
        {
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
            factory.SetBinding(TextBlock.TextProperty, new Binding(displayMemberPath));
            dataTemplate.VisualTree = factory;
            return dataTemplate;
        }

        #endregion

        #region ItemTemplate

        public static DataTemplate GetItemTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(ItemTemplateProperty);
        }

        public static void SetItemTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.RegisterAttached("ItemTemplate", typeof(DataTemplate), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetBindingsForContentControl)));

        #endregion

        #region Content

        public static object GetContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(object), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetBindingsForContentControl)));

        #endregion

        #region IsSelectedMemberPath

        public static string GetIsSelectedMemberPath(DependencyObject obj)
        {
            return (string)obj.GetValue(IsSelectedMemberPathProperty);
        }

        public static void SetIsSelectedMemberPath(DependencyObject obj, string value)
        {
            obj.SetValue(IsSelectedMemberPathProperty, value);
        }

        public static readonly DependencyProperty IsSelectedMemberPathProperty =
            DependencyProperty.RegisterAttached("IsSelectedMemberPath", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetIsSelectedBindingForCheckBox)));

        #endregion

        private static void SetIsSelectedBindingForCheckBox(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            CheckBox checkBox = dep as CheckBox;
            string bindingName = args.NewValue as string;

            if (checkBox != null && bindingName != null)
                checkBox.SetBinding(CheckBox.IsCheckedProperty, bindingName);
        }

        #region IsEnabledMemberPath

        public static string GetIsEnabledMemberPath(DependencyObject obj)
        {
            return (string)obj.GetValue(IsEnabledMemberPathProperty);
        }

        public static void SetIsEnabledMemberPath(DependencyObject obj, string value)
        {
            obj.SetValue(IsEnabledMemberPathProperty, value);
        }

        public static readonly DependencyProperty IsEnabledMemberPathProperty =
            DependencyProperty.RegisterAttached("IsEnabledMemberPath", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetIsEnabledBindingForCheckBox)));

        private static void SetIsEnabledBindingForCheckBox(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            CheckBox checkBox = dep as CheckBox;
            string bindingName = args.NewValue as string;

            if (checkBox != null && bindingName != null)
                checkBox.SetBinding(CheckBox.IsEnabledProperty, bindingName);
        }

        #endregion

        #region IsCheckBoxVisibleMemberPath

        public static string GetIsCheckBoxVisibleMemberPath(DependencyObject obj)
        {
            return (string)obj.GetValue(IsCheckBoxVisibleMemberPathProperty);
        }

        public static void SetIsCheckBoxVisibleMemberPath(DependencyObject obj, string value)
        {
            obj.SetValue(IsCheckBoxVisibleMemberPathProperty, value);
        }

        public static readonly DependencyProperty IsCheckBoxVisibleMemberPathProperty =
            DependencyProperty.RegisterAttached("IsCheckBoxVisibleMemberPath", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetIsVisibleBindingForCheckBox)));

        private static void SetIsVisibleBindingForCheckBox(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            CheckBox checkBox = dep as CheckBox;
            string bindingName = args.NewValue as string;

            if (checkBox != null && bindingName != null)
            {
                Binding visibilityBinding = new Binding(bindingName);
                visibilityBinding.Converter = new BooleanToVisibilityConverter();
                checkBox.SetBinding(CheckBox.VisibilityProperty, visibilityBinding);
            }
        }

        #endregion

        #region IsExpandedNodeMemberPathProperty

        public static string GetIsExpandedNodeMemberPath(DependencyObject obj)
        {
            return (string)obj.GetValue(IsExpandedNodeMemberPathProperty);
        }

        public static void SetIsExpandedNodeMemberPath(DependencyObject obj, string value)
        {
            obj.SetValue(IsExpandedNodeMemberPathProperty, value);
        }

        public static readonly DependencyProperty IsExpandedNodeMemberPathProperty =
            DependencyProperty.RegisterAttached("IsExpandedNodeMemberPath", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetIsExpandedBindingForTreeViewItem)));

        private static void SetIsExpandedBindingForTreeViewItem(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            TreeViewItem treeViewItem = dep as TreeViewItem;
            string bindingName = args.NewValue as string;

            if (treeViewItem != null && bindingName != null)
                treeViewItem.SetBinding(TreeViewItem.IsExpandedProperty, bindingName);
        }

        #endregion

        #region VisibilityBindingForStackPanel

        public static string GetVisibilityBindingForStackPanel(DependencyObject obj)
        {
            return (string)obj.GetValue(VisibilityBindingForStackPanelProperty);
        }

        public static void SetVisibilityBindingForStackPanel(DependencyObject obj, string value)
        {
            obj.SetValue(VisibilityBindingForStackPanelProperty, value);
        }

        public static readonly DependencyProperty VisibilityBindingForStackPanelProperty =
            DependencyProperty.RegisterAttached("VisibilityBindingForStackPanel", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetVisibilityBindingForStackPanel)));

        private static void SetVisibilityBindingForStackPanel(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            StackPanel stackPanel = dep as StackPanel;
            string bindingName = args.NewValue as string;

            if (stackPanel != null && bindingName != null)
            {
                Binding binding = new Binding(bindingName);
                binding.Converter = new ListToVisibilityInvertedConverter();
                stackPanel.SetBinding(StackPanel.VisibilityProperty, binding);
            }
        }

        #endregion

        #region VisibilityBindingForTreeViewItem

        public static string GetVisibilityBindingForTreeViewItem(DependencyObject obj)
        {
            return (string)obj.GetValue(VisibilityBindingForTreeViewItemProperty);
        }

        public static void SetVisibilityBindingForTreeViewItem(DependencyObject obj, string value)
        {
            obj.SetValue(VisibilityBindingForTreeViewItemProperty, value);
        }

        public static readonly DependencyProperty VisibilityBindingForTreeViewItemProperty =
            DependencyProperty.RegisterAttached("VisibilityBindingForTreeViewItem", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetVisibilityBindingForTreeViewItem)));


        private static void SetVisibilityBindingForTreeViewItem(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            TreeViewItem treeViewItem = dep as TreeViewItem;
            string bindingName = args.NewValue as string;

            if (treeViewItem != null && bindingName != null)
            {
                Binding binding = new Binding(bindingName);
                binding.Converter = new ListToVisibilityConverter();
                treeViewItem.SetBinding(TreeViewItem.VisibilityProperty, binding);
            }
        }

        #endregion

        public static string GetItemsSource(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(DependencyObject obj, string value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached("ItemsSource", typeof(string), typeof(SelectionComboBindingManager),
            new UIPropertyMetadata(null, new PropertyChangedCallback(SetItemsSourceBindingForItemsControl)));

        private static void SetItemsSourceBindingForItemsControl(DependencyObject dep, DependencyPropertyChangedEventArgs args)
        {
            ItemsControl itemsControl = dep as ItemsControl;
            string bindingName = args.NewValue as string;

            if (itemsControl != null && bindingName != null)
                itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, bindingName);
        }
    }

    class ListToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var list = value as System.Collections.IEnumerable;
            if (list != null && list.Cast<object>().Count() > 0)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    class ListToVisibilityInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var list = value as System.Collections.IEnumerable;
            if (list != null && list.Cast<object>().Count() > 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
