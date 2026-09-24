namespace ood_domotica_monitoring;

public abstract class Screen
{
    protected terminalHelper helper = new terminalHelper();
    protected bool running = true;

    protected abstract string Title { get; }
    protected abstract List<string> Options { get; }
    protected abstract void HandleOption(int selected);

    public void Loop()
    {
        while (running)
        {
            int selected = helper.handleTerminal(Options, Title, "Select option to continue");
            HandleOption(selected);
        }
    }
}