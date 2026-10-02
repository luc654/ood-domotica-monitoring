namespace ood_domotica_monitoring;

public abstract class Screen
{
    protected terminalHelper helper = new terminalHelper();
    protected bool running = true;

    protected abstract string Title { get; }
    protected abstract List<string> Options { get; }
    
    protected virtual string Description { get; set; }
    protected abstract void HandleOption(int selected);

    public void Loop()
    {
        while (running)
        {
            if (Description.Length == 0) { Description = "a"; };

            int selected = helper.handleTerminal(Options, Title, Description);
            HandleOption(selected);
        }
    }

    public void showFormattedString(string value)
    {
        List<string> returnButton = new List<string>() { "terug" };
        Program.GlobalContext.notification = "";
        helper.handleTerminal(returnButton, "", value);
    }
}