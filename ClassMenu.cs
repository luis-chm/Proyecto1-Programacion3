using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyecto1_Programacion3
{
    internal class ClassMenu
    {
        private static int opcion = 0; //atributo var global
        static public void Menu()
        {
            do
            {
                Console.WriteLine("\t      Proyecto 1 \t\n");
                Console.WriteLine("-------------Menu Principal-------------");
                Console.WriteLine("Opcion 1:  Inicializar Arreglos");
                Console.WriteLine("Opcion 2:  Realizar Pagos.");
                Console.WriteLine("Opcion 3:  Consultar Pagos");
                Console.WriteLine("Opcion 4:  Modificar Pagos");
                Console.WriteLine("Opcion 5:  Eliminar Pagos");
                Console.WriteLine("Opcion 6:  Submenú Reportes");
                Console.WriteLine("Opcion 7:  Salir");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Seleccione su opcion: ");
                int.TryParse(Console.ReadLine(), out opcion);// si ingresa una letra no va a dejar continuar
                switch (opcion)
                {
                    case 1: ClassPagos.Inicializar(); break;
                    case 2: ClassPagos.realizarPagos(); break;
                    case 3: ClassPagos.consultarPagos(0); break;
                    case 4: ClassPagos.modificarPagos(); break;
                    case 5: ClassPagos.eliminarPagos(0); break;
                    case 6: SubMenuReportes(); break;
                    case 7:
                        Console.WriteLine("¡Hasta pronto! Gracias");
                        Thread.Sleep(1500);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("+------------------------------------------+\r\n| ¡Opcion no valida! Elige una entre 1 y 7 |\r\n+------------------------------------------+");
                        break;
                }// fin switch
            } while (opcion != 7);
        }// fin metodo menu

        public static void SubMenuReportes()
        {
            Console.Clear();
            do
            {
                Console.WriteLine("-------------Submenu Reportes-------------");
                Console.WriteLine("Opcion 1:  Reporte Ver todos los pagos.");
                Console.WriteLine("Opcion 2:  Reporte Pagos por tipo de Servicio.");
                Console.WriteLine("Opcion 3:  Reporte Pagos por código de caja.");
                Console.WriteLine("Opcion 4:  Reporte Dinero Comisionado por servicios.");
                Console.WriteLine("Opcion 5:  Regresar al Menu Principal.");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Seleccione su opcion: ");
                int.TryParse(Console.ReadLine(), out opcion);// si ingresa una letra no va a dejar continuar
                switch (opcion)
                {
                    case 1: ClassPagos.reporteTodosPagos(); break;
                    case 2: ClassPagos.reporteTipoServicio(0); break;
                    case 3: ClassPagos.reporteCodigoCaja(0); break;
                    case 4: ClassPagos.reporteDineroComisionado(); break;
                    case 5:
                        Console.WriteLine("Regresando...");
                        Thread.Sleep(1500);
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("+------------------------------------------+\r\n| ¡Opcion no valida! Elige una entre 1 y 5 |\r\n+------------------------------------------+");
                        break;
                }//fin switch

            } while (opcion != 5);
        }// fin metodo Submenu

    }//fin clase
}//fin namespace
