using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models;

public class MainWorkspaceModel
{
    private readonly ILogger<MainWorkspaceModel> m_logger;
    public MainWorkspaceModel(ILogger<MainWorkspaceModel> p_logger)
    {
        m_logger                   = p_logger;
        m_logger.LogDebug("Instantiating MainWorkspaceModel");
    }
}