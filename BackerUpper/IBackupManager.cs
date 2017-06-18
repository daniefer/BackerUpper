using System.Threading;
using System.Threading.Tasks;

namespace BackerUpper
{
    public interface IBackupManager
    {
        Task Backup(CancellationToken cancellationToken);
    }
}