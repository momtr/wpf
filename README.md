# POS Exam - C# WPF

## 0 Connections
## 0.1 Scope Definition
```csharp
    xmlns:vm="clr-namespace:Calculator.ViewModel"
    xmlns:validation="clr-namespace:Registration.Validation"
```

## 0.2 View Model - View
```csharp
    <Window.DataContext>
        <vm:MainViewModel />
    </Window.DataContext>
```

## 0.3 Converter as Resource
```csharp
    <Window.Resources>
        <con:DateConverter x:Key="DateConverter" />
    </Window.Resources>
```

## 1 Layouts
### 1.1 Grid
```csharp
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="160"/>
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <TextBlock Grid.Row="0" Grid.Column="0">Test</TextBlock>
        <TextBlock Grid.Row="0" Grid.Column="1">Test</TextBlock>
        <TextBlock Grid.Row="1" Grid.Column="0">Test</TextBlock>
        <TextBlock Grid.Row="1" Grid.Column="1">Test</TextBlock>
    </Grid>
```

### 1.2 Stack Panel
vertical positioning
```csharp
    <StackPanel>
        ...
    </StackPanel>
```

## 2 Controls
### 2.1 TextBox
- text as user input / text input field
```csharp
    <TextBox />
```

### 2.2 TextBlock
- just a simple label
```csharp
    <TextBlock />
```

### 2.3 Slider
```csharp
    <Slider Minimum="0" Maximum="255" Margin="0,0,0,20" Value="{Binding R, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
```

### 2.4 DatePicker
```csharp
    <DatePicker Grid.Row="5" Grid.Column="1" SelectedDate="{Binding FirstSchoolDay, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}" HorizontalAlignment="Left" Margin="0,10.4,0,0" VerticalAlignment="Top" Width="385" Height="27"/>
```

### 2.5 RadioButton
```csharp
    <StackPanel Grid.Row="6" Grid.Column="1" Margin="0,46.4,0.2,0.2" >
        <RadioButton GroupName="Gender" Command="{Binding ChangeGender}" CommandParameter="MALE">Male</RadioButton>
        <RadioButton GroupName="Gender" Command="{Binding ChangeGender}" CommandParameter="FEMALE">Female</RadioButton>
        <RadioButton GroupName="Gender" Command="{Binding ChangeGender}" CommandParameter="OTHER">Other</RadioButton>
    </StackPanel>
```

### 2.6 ComboBox
```csharp
    <ComboBox FontSize="36" SelectedIndex="0" Margin="0,0,0,10" SelectedValue="{Binding OpCode, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" HorizontalContentAlignment="Center" SelectedValuePath="Content">
        <ComboBoxItem>+</ComboBoxItem>
        <ComboBoxItem>-</ComboBoxItem>
        <ComboBoxItem>*</ComboBoxItem>
        <ComboBoxItem>/</ComboBoxItem>
    </ComboBox>
```

### 2.7 Menu
- if menu item is clicked => trigger relay command defined in VM (= view model)
```csharp
    <Menu>
        <MenuItem Header="File">
            <MenuItem Header="Open" Command="{Binding OpenFileCommand}" />
            <Separator />
            <MenuItem Header="Exit" Command="{Binding ExitCommand}" />
        </MenuItem>
        <MenuItem Header="Help">
            <MenuItem Header="About" Command="{Binding AboutCommand}" />
        </MenuItem>
    </Menu>
```

## 3 Binding
### 3.1 Property change
```csharp
    private void UpdateProperty(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
```
example:
```csharp
    UpdateProperty("Result");
```
> Update in setters!

### 3.2 Property
```csharp
    public int A {
        get => _a;
        set
        {
            _a = value;
            UpdateProperty("Result");
            UpdateProperty("A");
        }
    }
```

## 3.3 XAML
- `Binding <PropertyName>`
- `Mode=TwoWay` if view should be able to manipulate/set the property
- `UpdateSourceTrigger=PropertyChanged`
- `ValidatesOnDataErrors=True` used together with IDateErrorInfo

### 3.3.1 XAML bindings as attributes
```csharp
    "{Binding FirstSchoolDay, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}"
```

### 3.3.1 XAML bindings as elements
- used together with validation rules
```csharp
    <TextBox Grid.Row="2" Grid.Column="1" Background="{DynamicResource {x:Static SystemColors.HotTrackBrushKey}}" Foreground="White" Margin="0,46.8,-0.8,46.8">
        <TextBox.Text>
            <Binding Path="Password" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged" ValidatesOnDataErrors="True">
                <Binding.ValidationRules>
                    <validation:PasswordValidation />
                </Binding.ValidationRules>
            </Binding>
        </TextBox.Text>
    </TextBox>
```

## 4 Commands
- use `RelayCommand.cs` => implements `ICommand`
```csharp
    public ICommand ExitCommand { get; }
    ...
    public MainViewModel()
    {
        ExitCommand = new RelayCommand(
            () =>
            {
                System.Windows.Application.Current.Shutdown();
            }
        );
        ...
    }
```

## 5 Converters
- implement `IValueConverter`
- `public object Convert(object value, Type targetType, object parameter, CultureInfo culture)` converts Property to View Type
- `public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)` converts View Type to Property
```csharp
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                return $"{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;
            try
            {
                var cultureInfo = new CultureInfo("en-US");
                return DateTime.Parse((string)value);
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
```

## 6 Validation
### 6.1 ValidationRule (View Validation)
- implement `ValidationRule`
- method: `public override ValidationResult Validate(object value, CultureInfo cultureInfo)`
```csharp
    class CharacterValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = (string)value;
            if(Regex.Match(val, @"^[A-Za-z]*$").Success)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "input contains invalid characters");
        }

    }
```

### 6.2 IDataErrorInfo (View Model Validation)
- validation in view model, if validation depends on multiple fields or is more complex
- - otherwiese, use validation rules
- implement interface `IDataErrorInfo`
```csharp
public string Error => throw new NotImplementedException();

public string this[string columnName]
{
    get
    {
        string res = String.Empty;
        if (columnName == "RepeatPassword")
        {
            if (Password != null && RepeatPassword != null && !Password.Equals(RepeatPassword))
            {
                res = "Passwords must match";
            }
        }
        if (columnName == "Password")
        {
            if (Password != null && RepeatPassword != null && !Password.Equals(RepeatPassword))
            {
                res = "Passwords must match";
            }
        }
        if (columnName == "FirstSchoolDay")
        {
            if (Birthday >= FirstSchoolDay)
            {
                res = "FirstSchoolDay must be after birthdate";
            }
        }
        return res;
    }
}
```

# Don't forget
- TwoWay bindings
- Trigger property changes
- validate binding attribute at IDateErrorInfo XAML
- IDataErrorInfo => trigger other property changes
