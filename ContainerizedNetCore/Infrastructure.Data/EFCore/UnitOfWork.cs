using System;

namespace Infrastructure.Data.EFCore
{
    public class UnitOfWork : IDisposable // TODO: Expose IUnitOfWork to Domain
    {
        internal ApplicationDbContext _context { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
