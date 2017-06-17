using System.Threading;
using System.Threading.Tasks;

namespace GlacierBackupService
{
    public interface IBackupManager
    {
        Task Backup(CancellationToken cancellationToken);
    }
}