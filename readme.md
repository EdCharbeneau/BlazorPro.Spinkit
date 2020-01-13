# BlazorPro Spinkit

A collection of loading indicators animated with CSS for Blazor. Spinkit also includes the SpinLoader component with templates for handling null values during async operations. Spinkit includes CSS from the Spinkit project by [Tobias Ahlin](https://tobiasahlin.com/spinkit/)

![Screenshot of the component in action](https://raw.githubusercontent.com/EdCharbeneau/BlazorPro.Spinkit/master/src/BlazorPro.Spinkit/_resources/demo.gif)

## Getting Setup

You can install the package via the nuget package manager just search for *BlazorPro.Spinkit*. You can also install via powershell using the following command.

```powershell
Install-Package BlazorPro.Spinkit
```

Or via the dotnet CLI.

```bash
dotnet add package BlazorPro.Spinkit
```

### 1. Add Imports

Add the following to your *_Imports.razor*

```csharp
@using BlazorPro.Spinkit
```

### 2. Add a spinner component

Add one of the following components to your index page.

```
<Chase />
<Circle />
<CircleFade />
<Flow />
<Grid />
<Plane />
<Pulse />
<Swing />
<Wander />
<Wave />
<Bounce />
```
### 3. Add reference to style sheet

Add the following line to the `head` tag of your `_Host.cshtml` (Blazor Server) or `index.html` (Blazor WebAssembly).

```html
    <link href="_content/BlazorPro.Spinkit/spinkit.min.css" rel="stylesheet" />
```

## Usage

### Displaying a conditional spinner

Spinners are best used with some long running task. Simply create a flag to indicate the process is running and display a spinner with an `if` statement.

```html
@page "/"

if (IsLoading) {
	<Pulse />
}

```

### Displaying an integrated loading experience with the SpinLoader Component

Instead of writing common boilerplate code with if statements you may use the SpinLoader Component. With SpinLoader there is no need to write `if` statements and check the data source for null values.

**Before SpinLoader**

In the following example we can remove the statement `@if (forecasts == null)` and replace it with the SpinLoader Component.

```
@page "/fetchdata"
@inject HttpClient Http

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>

@if (forecasts == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var forecast in forecasts)
            {
                <tr>
                    <td>@forecast.Date.ToShortDateString()</td>
                    <td>@forecast.TemperatureC</td>
                    <td>@forecast.TemperatureF</td>
                    <td>@forecast.Summary</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
```

**After SpinLoader**

In the following example the loading predicate `forecast == null` is moved to the IsLoading property of the SpinLoader Component. This eliminates the need for an `if` statement. A default loading indicator (aka spinner) will be shown or one can be chosen by setting the `Spinner` property, Ex: `Spinner="SpinnerType.Plane"`.

```
@page "/fetchdata"
<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>
<p>An artificial delay of 3000ms is used to show a &lt;SpinLoader&gt; component.</p>

<SpinLoader IsLoading="@(forecasts == null)">
    <ContentTemplate>
        <table class="table">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var forecast in forecasts)
                {
                    <tr>
                        <td>@forecast.Date.ToShortDateString()</td>
                        <td>@forecast.TemperatureC</td>
                        <td>@forecast.TemperatureF</td>
                        <td>@forecast.Summary</td>
                    </tr>
                }
            </tbody>
        </table>
    </ContentTemplate>
</SpinLoader>

@code {

    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
```


## Customiztion

### Spinner Customization

Spinners have three parameters that are used to control their properties: Color (string), Size (string), and Center (bool).

**Color:** Any valid CSS color value. 

```
All Black
<Circle Color="#000000" />

Green with Opacity
<Circle Color="rgba(0,255,0,0.3)"/>
```

**Size:** Any valid CSS size value. `[ px | em | rem | % ]`

```
Pixels
<Circle Size="100px" />
```

**Center:** When set to true the spinner renders with `margin: auto`

### SpinLoader Customization

The SpinLoader Component has the same properties as a Spinner: Color (string), Size (string), and Cetner (bool). These values will be passed to the SpinLoader's internal spinner if **no LoadingTemplate** is specified.

<p>
    The &lt;SpinLoader&gt; component supports three template regions:
    <ul>
        <li>LoadingTemplate (optional) - content to display while the IsLoading property is <b>true</b>.</li>
        <li>ContentTemplate - content to display when the IsLoading is <b>false</b></li>
        <li>FaultedTemplate - content to display when IsFaulted is <b>true</b> and IsLoading is <b>false</b></li>
    </ul>
</p>

****

### Advanced Customization

Spinners can be globally styled using CSS through the `--sk-color` and `--sk-size` CSS variables.

```
:root {
    --sk-size: 40px;
    --sk-color: #333;
}
```

A more targeted approach is to use a CSS selector with a greater specificity.

```
site.css
.my-spinner {
    --sk-size: 48px;
    --sk-color: #003366;
}

Component.razor
<Circle css="my-spinner" />
```

### Advanced: App Loader

Even though we don't have access to Razor Components in our index.html, we can still make use of the component's HTML/CSS because it is based on the popular CSS library Spinkit by [Tobias Ahlin](https://tobiasahlin.com/spinkit/).

Note: This feature is not useful for Blazor Server apps since they are pre-rendered.

1. Inside the index.html replace the `<app>` element with the following snippet.

```
<app>
    <div class="modal-overlay">
        <div class="sk-wave">
            <div class="sk-wave-rect"></div>
            <div class="sk-wave-rect"></div>
            <div class="sk-wave-rect"></div>
            <div class="sk-wave-rect"></div>
            <div class="sk-wave-rect"></div>
        </div>
    </div>
</app>
```

2. Add the `modal-overlay` style to your site.css.

```
.modal-overlay {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0,0,0,0.5);
    z-index: 2000;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    --sk-color: white;
}
```

To choose the spinner type refer to the [HTML/CSS project's documentation](https://github.com/tobiasahlin/SpinKit).

## Full Featured Example

The following example shows the full extent of what SpinLoader can do. This example includes all possible templates and pattern for handling exceptions.

```
@page "/fetchdata"
@inject HttpClient Http

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>
<p>
    The &lt;SpinLoader&gt; component supports three template regions:
    <ul>
        <li>LoadingTemplate (optional) - content to display while the IsLoading property is <b>true</b>.</li>
        <li>ContentTemplate - content to display when the IsLoading is <b>false</b></li>
        <li>FaultedTemplate - content to display when IsFaulted is <b>true</b> and IsLoading is <b>false</b></li>
    </ul>
</p>
<div class="form-row align-items-center">
    <div class="col-auto">
        <div class="form-check mb-2">
            <input id="forceException" type="checkbox" @bind="forceException" class="form-check-input" />
            <label class="form-check-label" for="forceException">Force exception</label>
        </div>
    </div>
    <div class="col-auto">
        <button class="btn btn-primary mb-2" @onclick="LoadData">Retry</button>
    </div>
</div>
<table class="table">
    <thead>
        <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
        </tr>
    </thead>
    <tbody>
        <SpinLoader IsLoading="isLoading" IsFaulted="isFaulted">
            <LoadingTemplate>
                <tr>
                    <td colspan="4" style="vertical-align: middle;background-color: rgb(0, 0, 0, .2); height:300px;">
                        <Circle Color="#e67e22" Size="60px" Center="true" />
                    </td>
                </tr>
            </LoadingTemplate>
            <ContentTemplate>
                @foreach (var forecast in forecasts)
                {
                    <tr>
                        <td>@forecast.Date.ToShortDateString()</td>
                        <td>@forecast.TemperatureC</td>
                        <td>@forecast.TemperatureF</td>
                        <td>@forecast.Summary</td>
                    </tr>
                }
            </ContentTemplate>
            <FaultedContentTemplate>
                <tr>
                    <td colspan="4">
                        <div class="alert alert-danger">Fail</div>
                    </td>
                </tr>
            </FaultedContentTemplate>
        </SpinLoader>
    </tbody>
</table>

@code {

    WeatherForecast[] forecasts;
    bool isFaulted = false;
    bool isLoading = true;
    int delay = 2000;
    bool forceException = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    async Task LoadData()
    {
        await TryLoadingData(
            onSuccess: SuccessPath,
            onFaulted: FaultedPath
            );
    }

    void SuccessPath(WeatherForecast[] data)
    {
        // do work
        forecasts = data;
    }

    void FaultedPath(Exception e)
    {
        // log message, don't share it with the user
        var fakeLog = e.Message;
    }

    async Task TryLoadingData(Action<WeatherForecast[]> onSuccess, Action<Exception> onFaulted)
    {
        isLoading = true;
        await Task.Delay(delay);
        try
        {
            if (forceException)
            {
                throw new NotSupportedException();
            }
            var data = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            isFaulted = false;
            onSuccess(data);
        }
        catch (Exception e)
        {
            isFaulted = true;
            onFaulted(e);
        }
        finally
        {
            isLoading = false;
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
```

## Server Operation

Because Blazor Server apps use pre-rendering long operations may need to be wrapped in Task.Run to invoke a spinner.

```
await Task.Run(async ()=> await WeatherService.GetForecastAsync(DateTime.Now))
```