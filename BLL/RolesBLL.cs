using Microsoft.EntityFrameworkCore;
using Registro_De_Roles.DAL;
using Registro_De_Roles.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Registro_De_Roles.BLL
{
    public class RolesBLL
    {
        public static bool Guardar(Roles rol)
        {
            if (!Existe(rol.RolId))
                return Insertar(rol);
            else
                return Modificar(rol);
        }

        private static bool Existe(int RolId)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Roles.Any(x => x.RolId == RolId);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Insertar(Roles rol)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                foreach (var item in rol.Detalle)
                {
                    contexto.Entry(item.Permisos).State = EntityState.Modified;
                }
                contexto.Roles.Add(rol);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Modificar(Roles rol)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM DetalleRoles WHERE RolId ={rol.RolId}");
                foreach (var item in rol.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(rol).State = EntityState.Modified;
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static Roles Buscar(int RolId)
        {
            Contexto contexto = new Contexto();
            Roles rol;
            try
            {
                rol = contexto.Roles
                    .Where(x => x.RolId == RolId)
                    .Include(d => d.Detalle)
                    .ThenInclude(d => d.Permisos)
                    .SingleOrDefault();//Busca el registro en la base de datos.
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return rol;
        }

        public static bool Eliminar(int RolId)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var item = Buscar(RolId);
                if (item != null)
                {
                    contexto.Roles.Remove(item);
                    ok = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static List<Roles> GetList(Expression<Func<Roles, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Roles> lista = new List<Roles>();

            try
            {
                lista = contexto.Roles.Where(criterio).Include(X => X.Detalle).ThenInclude(d => d.Permisos).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
