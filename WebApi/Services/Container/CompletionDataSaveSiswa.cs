using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Container
{
    internal static class CompletionDataSaveSiswa
    {
        internal static void Save(ref CompleteDataSiswa container)
        {
            using(var scope = new TransactionScope())
            {
                string id_siswa = container.siswa.id_siswa;

                container.repository.SiswaRepository.Update(container.siswa);
                container.repository.Save();

                scope.Complete();
            }
        }
    }
}
