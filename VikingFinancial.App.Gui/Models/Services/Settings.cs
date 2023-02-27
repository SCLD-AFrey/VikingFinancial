using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Services;
public class Settings
{
    private readonly ILogger<Settings>? m_logger;
    private readonly Files? m_requiredFilesService;

    [JsonConstructor]
    public Settings(bool hasConnectedPreviously)
    {
        HasConnectedPreviously = hasConnectedPreviously;
    }


    /// <summary>
    /// Settings
    /// </summary>
    public bool HasConnectedPreviously { get; set; } = false;

    public async Task WriteSettings()
    {
        try
        {
            await using var fs = File.Create(m_requiredFilesService!.SettingsFilePath);

            await JsonSerializer.SerializeAsync(fs, this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await fs.FlushAsync();
            fs.Close();
        }
        catch (Exception e)
        {
            m_logger!.LogError("Failed to save to settings due to {ErrorMessage}", e.Message);
        }
    }

    public async Task ReadSettings()
    {
        if (!File.Exists(m_requiredFilesService!.SettingsFilePath))
        {
            await WriteSettings();
        }

        await using var storedSettings = File.OpenRead(m_requiredFilesService.SettingsFilePath);
        var settings = await JsonSerializer.DeserializeAsync<Settings>(storedSettings);

        if (settings is null) return;

        HasConnectedPreviously = settings.HasConnectedPreviously;

        await storedSettings.FlushAsync();
        storedSettings.Close();
    }



}

