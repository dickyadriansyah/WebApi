using Entites.Data;
using Services.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Operations
{
    internal class CompleteDataOperationsSiswa
    {

        internal static void Initialize(ref CompleteDataSiswa container)
        {
            var siswa = container.siswa;

            Save(container, siswa);
        }


        private static void Save(CompleteDataSiswa container, siswa siswa)
        {
            using(var scope = new TransactionScope())
            {
                container.repository.SiswaRepository.Insert(siswa);
                container.repository.Save();

                scope.Complete();
            }
        }

    }
}
