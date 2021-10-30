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
    public class PermisosBLL
    {
        public static bool Guardar(Permisos Permiso)
        {
            if (!Existe(Permiso.PermisoId))
                return Insertar(Permiso);
            else
                return Modificar(Permiso);
        }

        private static bool Existe(int PermisoId)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Permisos.Any(x => x.PermisoId == PermisoId);
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

        private static bool Insertar(Permisos Permiso)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Permisos.Add(Permiso);
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

        private static bool Modificar(Permisos Permiso)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(Permiso).State = EntityState.Modified;
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

        public static Permisos Buscar(int PermisoId)
        {
            Contexto contexto = new Contexto();
            Permisos Permiso;
            try
            {
                Permiso = contexto.Permisos.Find(PermisoId);//Busca el registro en la base de datos.
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Permiso;
        }

        public static bool Eliminar(int PermisoId)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var item = Buscar(PermisoId);
                if (item != null)
                {
                    contexto.Permisos.Remove(item);
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

        public static List<Permisos> GetList(Expression<Func<Permisos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Permisos> lista = new List<Permisos>();

            try
            {
                lista = contexto.Permisos.Where(criterio).ToList();
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

        public static void Incrementar(int id)
        {

            Permisos permiso;
            try
            {
                permiso = Buscar(id);
                permiso.VecesAsignado++;
                Modificar(permiso);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void Decrementar(int id)
        {

            Permisos permiso;
            try
            {
                permiso = Buscar(id);
                permiso.VecesAsignado--;
                Modificar(permiso);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
