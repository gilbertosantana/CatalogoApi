﻿using CatalogoApi.Context;

namespace CatalogoApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProdutoRepository? _produtoRepository;
        private CategoriaRepository? _categoriaRepository;
        public CatalogoApiContext _context;
        public UnitOfWork(CatalogoApiContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context); 
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);    
            }
        }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
