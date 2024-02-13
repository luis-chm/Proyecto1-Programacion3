using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Proyecto1_Programacion3
{
    internal class ClassPagos
    {
        //atributos de pagos por vectores
        public static int cantidad = 10;
        static private int[] numPago = new int[cantidad];
        static private DateTime[] fecha = new DateTime[cantidad];
        static private TimeSpan[] hora = new TimeSpan[cantidad];
        static private string[] cedula = new string[cantidad];
        static private string[] nombre = new string[cantidad];
        static private string[] apellido1 = new string[cantidad];
        static private string[] apellido2 = new string[cantidad];
        static private int[] numCaja = new int[cantidad];
        static private int[] tipoServicio = new int[cantidad];
        static private int[] numFactura = new int[cantidad];
        static private float[] montoPagar = new float[cantidad];
        static private float[] montoComision = new float[cantidad];
        static private float[] montoDeducido = new float[cantidad];
        static private float[] montoPagaCliente = new float[cantidad];
        static private float[] vuelto = new float[cantidad];
        private static Boolean idEncontrado;
        private static char respuesta;
        private static float totalComisionado;
        private static string fechaActual;
        private static string horaActual;
        private static int indice;

        //metodos
        public static void Inicializar()
        {
            //enumerable.repeat asigna valores iniciales a cada elemento del vector (remplaza un for)
            indice = 0;
            numPago = Enumerable.Repeat(0, cantidad).ToArray();
            fecha = Enumerable.Repeat(DateTime.Now, cantidad).ToArray();
            hora = Enumerable.Repeat(DateTime.Now.TimeOfDay, cantidad).ToArray();
            cedula = Enumerable.Repeat("", cantidad).ToArray();
            nombre = Enumerable.Repeat("", cantidad).ToArray();
            apellido1 = Enumerable.Repeat("", cantidad).ToArray();
            apellido2 = Enumerable.Repeat("", cantidad).ToArray();
            numCaja = Enumerable.Repeat(0, cantidad).ToArray();
            tipoServicio = Enumerable.Repeat(0, cantidad).ToArray();
            numFactura = Enumerable.Repeat(0, cantidad).ToArray();
            montoPagar = Enumerable.Repeat(0.0f, cantidad).ToArray();
            montoComision = Enumerable.Repeat(0.0f, cantidad).ToArray();
            montoDeducido = Enumerable.Repeat(0.0f, cantidad).ToArray();
            montoPagaCliente = Enumerable.Repeat(0.0f, cantidad).ToArray();
            vuelto = Enumerable.Repeat(0.0f, cantidad).ToArray();
            //Inicializar el vector numCaja con números aleatorios del 1 al 3

            /*Console.WriteLine("Cargando arreglos al programa.");
            Console.WriteLine("Por favor espere...");
            Thread.Sleep(2000);
            Console.WriteLine("Arreglos inicializados con exito.\n");
            Thread.Sleep(1500);*/
            Console.Clear();
        }//fin incializar

        public static void realizarPagos()
        {
            respuesta = 'N';
            bool continua;
            do
            {
                continua = false;
                do
                {
                    try
                    {
                        if (indice < cantidad)
                        {
                            fechaActual = obtenerFechaActual();
                            horaActual = obtenerHoraActual();
                            Console.Clear();
                            Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                            numPago[indice] = indice + 1;
                            Console.WriteLine("\nNúmero de pago: " + numPago[indice]);
                            Console.WriteLine("Fecha " + fechaActual + " hora " + horaActual);
                            Console.WriteLine("────────────────────────────────────");
                            addCedula();
                            nombre[indice] = obtenerTexto("Digite el nombre del cliente: ");
                            apellido1[indice] = obtenerTexto("Digite el primer apellido: ");
                            apellido2[indice] = obtenerTexto("Digite el segundo apellido: ");
                            Console.Clear();
                            Console.WriteLine("────────────────────────────────────");
                            Random random = new Random();
                            numCaja[indice] = random.Next(1, 4);
                            Console.WriteLine("Número de Caja asignada: #" + numCaja[indice]);
                            tipoServicio[indice] = obtenerInt($"\nTipo de Servicio a pagar:\n1- Recibo de Luz.\n2- Recibo Teléfono.\n3- Recibo de Agua.\n\nDigite un número del 1 a 3: ");
                            Console.Clear();
                            Console.WriteLine("────────────────────────────────────");
                            numFactura[indice] = obtenerInt("Digite el numero de factura: ");
                            Console.WriteLine("────────────────────────────────────");
                            montoPagar[indice] = obtenerFloat("Digite el monto a pagar: ");
                            Console.Clear();
                            calcComision(indice);
                            montoDeducido[indice] = montoPagar[indice] - montoComision[indice];
                            Console.WriteLine("────────────────────────────────────");
                            Console.WriteLine($"Monto deducido a: " + montoDeducido[indice]);
                            do
                            {
                                montoPagar[indice] = montoDeducido[indice];
                                montoPagaCliente[indice] = obtenerFloat("Ingrese el monto a pagar por el cliente: ");

                                if (montoPagaCliente[indice] < montoPagar[indice])
                                {
                                    Console.WriteLine("¡Cuidado! Monto pagado es menor al monto a pagar.");
                                }

                            } while (montoPagaCliente[indice] < montoPagar[indice]);

                            vuelto[indice] = montoPagaCliente[indice] - montoPagar[indice];
                            Console.WriteLine("\nVuelto: " + vuelto[indice]);
                            Console.WriteLine("────────────────────────────────────");
                            Console.WriteLine("------- TIENDA LA FAVORITA -------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nPago realizado con exito.\n");
                            Console.ResetColor();
                            Console.WriteLine("Desea realizar otro pago (S/N)");
                            while (!char.TryParse(Console.ReadLine(), out respuesta) || (respuesta = char.ToUpper(respuesta)) != 'S' && respuesta != 'N')
                            {
                                Console.WriteLine("Por favor, ingrese 'S' o 'N':");
                            }
                            Console.Clear();
                            indice++;

                        }//fin if
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("El arreglo se encuentra lleno. No se pueden agregar mas pagos");
                            Console.ResetColor();
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite el formato correcto.");
                        continua = true;
                    }
                } while (continua);
            } while (respuesta != 'N');
        }//fin metodo realizarPagos
        #region METODOS PARA REALIZAR PAGOS
        public static void calcComision(int indice)
        {
            float porcentajeComision = 0.0f;

            switch (tipoServicio[indice])
            {
                case 1: //Recibo electricidad
                    porcentajeComision = 0.04f;
                    break;
                case 2: //Recibo telefonico
                    porcentajeComision = 0.055f;
                    break;
                case 3: //Recibo agua
                    porcentajeComision = 0.065f;
                    break;
                default:
                    Console.WriteLine("Tipo de servicio no válido. No se aplicará comisión.");
                    break;
            }
            montoComision[indice] = montoPagar[indice] * porcentajeComision;
        }//fin metodo calcular comision
        public static void addCedula()
        {
            bool cedulaExistente;
            string nuevaCedula;
            do
            {
                Console.WriteLine($"Digite el número de cedula: ");
                nuevaCedula = Console.ReadLine();
                int.Parse(nuevaCedula);
                cedulaExistente = cedula.Contains(nuevaCedula);
                if (cedulaExistente)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLa cedula ya se encuentra registrada. Por favor digite otra.");
                    Console.ResetColor();
                }

            } while (cedulaExistente);
            cedula[indice] = nuevaCedula;
        }//fin metodo añadir y validar cedula
        public static string obtenerTexto(string mensaje)
        {
            string texto;
            do
            {
                Console.WriteLine(mensaje);
                texto = Console.ReadLine();
                if (!validarLetras(texto))
                {
                    Console.WriteLine("Por favor, digite solamente letras.");
                }
            } while (!validarLetras(texto));

            return texto;
        }//fin metodo get text
        public static bool validarLetras(string texto)
        {
            return Regex.IsMatch(texto, "^[a-zA-Z]+$");// valida si el texto ingresado hace match con la expresion ^[a-zA-Z]+$
            //^ inicio cadena, a-zA-Z: letras desde mayus a minus, $ fin cadena
            //REGEX: es una secuencia de caracteres que define un patrón de búsqueda
        }//fin metodo check text
        public static float obtenerFloat(string mensaje)
        {
            float monto;
            bool num;
            do
            {
                Console.WriteLine(mensaje);
                num = float.TryParse(Console.ReadLine(), out monto);

                if (!num)
                {
                    Console.WriteLine("Por favor, digite un número válido.");
                }
            } while (!num);

            return monto;
        }//fin metodo get float
        public static int obtenerInt(string mensaje)
        {
            int monto;
            bool num;
            do
            {
                Console.WriteLine(mensaje);
                num = int.TryParse(Console.ReadLine(), out monto);

                if (!num)
                {
                    Console.WriteLine("Por favor, digite un número válido.");
                }
            } while (!num);

            return monto;
        }//fin metodo get int
        public static string obtenerFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("d");
            return fechaFormateada;
        }//fin metodo get fecha
        public static string obtenerHoraActual()
        {
            DateTime horaActual = DateTime.Now;
            //formato de 24 horas
            string horaFormateada = horaActual.ToString("HH':'mm");
            return horaFormateada;
        }//fin metodo get hora
        #endregion
        public static void consultarPagos(int idPago)
        {
            respuesta = 'N';
            bool continua;
            do
            {
                continua = false;
                do
                {
                    try
                    {
                        idEncontrado = false;
                        Console.Clear();
                        Console.WriteLine("\n------------- Consultar Pagos -------------");

                        Console.WriteLine("\nDigite el número de pago que desea consultar:");
                        idPago = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < cantidad; i++)
                        {
                            if (idPago.Equals(numPago[i]))
                            {
                                Console.Clear();
                                Console.WriteLine($"\nDatos Encontrado Posicion Vector ({i}).\n");
                                Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                                Console.WriteLine($"\r\n  N° Pago: {numPago[i]}               ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Fecha: {fecha[i]}  Hora: {hora[i]}  ");
                                Console.WriteLine($"\r\n  Cedula: {cedula[i]}                 ");
                                Console.WriteLine($"\r\n  Nombre Completo: {nombre[i]} {apellido1[i]} {apellido2[i]} ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Tipo de Servicio: {tipoServicio[i]}     [1- Electricidad 2-Telefono 3-Agua] ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  N° de factura: {numFactura[i]}            Monto Pagar: {montoPagar[i]}   ");
                                Console.WriteLine($"\r\n  Comision autorizada: {montoComision[i]}       Paga con: {montoPagaCliente[i]}   ");
                                Console.WriteLine($"\r\n  Monto deducido: {montoDeducido[i]}          Vuelto {vuelto[i]}   ");
                                Console.WriteLine("\r\n ==================================== \r\n\r\n");
                                idEncontrado = true;
                            }
                        }
                        if (!idEncontrado)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nN° de pago no encontrada...\n");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Desea realizar otro consulta (S/N)");
                        while (!char.TryParse(Console.ReadLine(), out respuesta) || (respuesta = char.ToUpper(respuesta)) != 'S' && respuesta != 'N')
                        {
                            Console.WriteLine("Por favor, ingrese 'S' o 'N'.");
                        }
                        Console.Clear();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite el formato correcto.");
                        continua = true;
                    }
                } while (continua);
            } while (respuesta != 'N');

        }//fin metodo consultar
        public static void modificarPagos()
        {
            int cod = 0;
            Boolean codFound = false;
            Console.Clear();
            Console.WriteLine("\n------------- Modificar Pagos -------------");
            Console.WriteLine("\nDigite el número de pago que desea modificar:");
            cod = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < cantidad; i++)
            {
                if (cod.Equals(numPago[i]))
                {
                    Console.Clear();
                    int opcion = 0;

                    do
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine($"-------------Modificando N° Pago: {numPago[i]} -------------\n");
                        Console.WriteLine("Opcion 1:  Modificar Fecha.");
                        Console.WriteLine("Opcion 2:  Modificar Hora.");

                        Console.WriteLine("Opcion 3:  Modificar Cedula.");
                        Console.WriteLine("Opcion 4:  Modificar Nombre.");
                        Console.WriteLine("Opcion 5:  Modificar Primer Apellido.");
                        Console.WriteLine("Opcion 6:  Modificar Segundo Apellido.");

                        Console.WriteLine("Opcion 7:  Modificar Numero de caja.");
                        Console.WriteLine("Opcion 8:  Modificar Numero de factura.");
                        Console.WriteLine("Opcion 9:  Modificar Tipo de Servicio, Monto a Pagar.");

                        Console.WriteLine("Opcion 10: Modificar Monto a Pagar por cliente.");

                        Console.WriteLine("Opcion 11: Regresar al Menu Principal.");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Seleccione su opcion: ");
                        int.TryParse(Console.ReadLine(), out opcion);// si ingresa una letra no va a dejar continuar
                        switch (opcion)
                        {
                            case 1:
                                Console.WriteLine($"Fecha actual: {fechaActual}");
                                Console.WriteLine("Ingrese la nueva fecha en formato dd/MM/yyyy (01/02/2024):");
                                fechaActual = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nueva fecha: {fechaActual}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Fecha actualizada\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                             case 2:
                                Console.WriteLine($"Hora actual: {horaActual}");
                                Console.WriteLine("Ingrese la nueva hora en formato 24 horas:");
                                horaActual = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nueva fecha: {horaActual}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Fecha actualizada\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 3:
                                Console.WriteLine($"N° Cedula actual: {cedula[i]}");
                                Console.WriteLine("Ingrese el nuevo N° de cedula:");
                                cedula[i] = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nuevo N° de cedula: {cedula[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Cedula actualizada\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 4:
                                Console.WriteLine($"Nomnbre actual: {nombre[i]}");
                                Console.WriteLine("Ingrese el nuevo nombre del cliente:");
                                nombre[i] = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nuevo Nombre: {nombre[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Nombre actualizado\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 5:
                                Console.WriteLine($"1er Apellido actual: {apellido1[i]}");
                                Console.WriteLine("Ingrese el nuevo 1er Apellido:");
                                apellido1[i] = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nuevo 1er Apellido: {apellido1[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("1er Apellido actualizado\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 6:
                                Console.WriteLine($"2do Apellido actual: {apellido2[i]}");
                                Console.WriteLine("Ingrese el nuevo 2do Apellido:");
                                apellido2[i] = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"Nuevo 1er Apellido: {apellido2[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("2do Apellido actualizado\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 7:
                                Console.WriteLine($"N° de caja actual: {numCaja[i]}");
                                numCaja[i] = obtenerInt("Ingrese el nuevo N° de caja:");
                                Console.Clear();
                                Console.WriteLine($"Nuevo N° de caja: {numCaja[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("N° de caja actualizado\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 8:
                                Console.WriteLine($"N° de factura actual: {numFactura[i]}");
                                numFactura[i] = obtenerInt("Ingrese el nuevo N° de factura:");
                                Console.Clear();
                                Console.WriteLine($"Nuevo N° de factura: {numFactura[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("N° de factura actualizado\n");
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 9:
                                Console.WriteLine($"Tipo de Servicio actual: {tipoServicio[i]}");
                                tipoServicio[i] = obtenerInt($"\nIngrese el nuevo Tipo de Servicio:\n1- Recibo de Luz.\n2- Recibo Teléfono.\n3- Recibo de Agua.\n\nDigite un número del 1 a 3: ");
                                Console.Clear();
                                Console.WriteLine($"Nuevo Tipo de Servicio: {tipoServicio[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Tipo de Servicio actualizado\n");
                                Console.ResetColor();

                                Console.WriteLine($"Monto a pagar actual: {montoPagar[i]}");
                                montoPagar[i] = obtenerFloat("Ingrese el nuevo Monto a pagar:");
                                Console.Clear();
                                Console.WriteLine($"Nuevo Monto a pagar: {montoPagar[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Monto a pagar actualizado\n");
                                Console.ResetColor();
                                calcComision(i);
                                montoDeducido[i] = montoPagar[i] - montoComision[i];
                                Console.WriteLine("Se esta aplicando el nuevo rebajo por comision...");
                                Thread.Sleep(1500);
                                Console.WriteLine($"Nuevo monto deducido a: " + montoDeducido[i]);
                                Console.WriteLine("\nDirigirse a la opcion 10 para actualizar el pago del cliente");
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 10:
                                do
                                {
                                    montoPagar[i] = montoDeducido[i];
                                    Console.WriteLine($"Monto a pagar por el cliente actual: {montoDeducido[i]}");
                                    montoPagaCliente[i] = obtenerFloat("Ingrese el Nuevo monto a pagar por el cliente: ");

                                    if (montoPagaCliente[i] < montoPagar[i])
                                    {
                                        Console.WriteLine("Monto pagado por el cliente no puede ser menor al monto a pagar. Intente de nuevo.");
                                    }

                                } while (montoPagaCliente[i] < montoPagar[i]);

                                Console.WriteLine($"Nuevo monto a pagar por el cliente: {montoPagaCliente[i]}");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Monto a pagar por el cliente actualizado\n");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                vuelto[i] = montoPagaCliente[i] - montoPagar[i];
                                Console.WriteLine("Nuevo Vuelto: " + vuelto[i]);
                                Console.ResetColor();
                                Console.WriteLine("\nPresione Enter para volver al menú.");
                                Console.ReadLine();
                                Console.Clear();
                                break;

                            case 11:
                                Console.WriteLine("Regresando...");
                                Thread.Sleep(1500);
                                Console.Clear();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("+------------------------------------------+\r\n| ¡Opcion no valida! Elige una entre 1 y 12 |\r\n+------------------------------------------+");
                                Thread.Sleep(1500);
                                Console.Clear();
                                break;
                        }//fin switch

                    } while (opcion != 11);

                    codFound = true;
                    break;
                }
            }
            if (!codFound)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nN° de pago no encontrada...");
                Console.ResetColor();
                Console.WriteLine("\n\nRegresando al menu");
                Thread.Sleep(3000);
                Console.Clear();
            }


        }//fin metodo modificar
        public static void eliminarPagos(int idPago)
        {
            respuesta = 'N';
            char respuesta2 = 'N';
            bool continua;
            do
            {
                continua = false;
                do
                {
                    try
                    {
                        idEncontrado = false;
                        Console.Clear();
                        Console.WriteLine("\n------------- Eliminar Pagos -------------");
                        Console.WriteLine("\nDigite el número de pago que desea eliminar:");
                        idPago = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < cantidad; i++)
                        {
                            if (idPago.Equals(numPago[i]))
                            {
                                Console.Clear();
                                Console.WriteLine($"\nDatos Encontrado Posicion Vector ({i}).\n");
                                Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                                Console.WriteLine($"\r\n  N° Pago: {numPago[i]}               ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Fecha: {fecha[i]}  Hora: {hora[i]}  ");
                                Console.WriteLine($"\r\n  Cedula: {cedula[i]}                 ");
                                Console.WriteLine($"\r\n  Nombre Completo: {nombre[i]} {apellido1[i]} {apellido2[i]} ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Tipo de Servicio: {tipoServicio[i]}     [1- Electricidad 2-Telefono 3-Agua] ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  N° de factura: {numFactura[i]}            Monto Pagar: {montoPagar[i]}   ");
                                Console.WriteLine($"\r\n  Comision autorizada: {montoComision[i]}       Paga con: {montoPagaCliente[i]}   ");
                                Console.WriteLine($"\r\n  Monto deducido: {montoDeducido[i]}          Vuelto {vuelto[i]}   ");
                                Console.WriteLine("\r\n ==================================== \r\n\r\n");

                                Console.WriteLine("Esta seguro de eliminar el dato S/N?");
                                while (!char.TryParse(Console.ReadLine(), out respuesta2) || (respuesta2 = char.ToUpper(respuesta2)) != 'S' && respuesta2 != 'N')
                                {
                                    Console.Write("Por favor, ingrese 'S' o 'N': ");
                                }
                                Console.WriteLine();

                                if (respuesta2.ToString().ToUpper() == "S")
                                {
                                    //eliminar datos
                                    numPago[i] = 0;
                                    fecha[i] = DateTime.MinValue;
                                    hora[i] = TimeSpan.MinValue;
                                    cedula[i] = "";
                                    nombre[i] = "";
                                    apellido1[i] = "";
                                    apellido2[i] = "";
                                    numCaja[i] = 0;
                                    tipoServicio[i] = 0;
                                    numFactura[i] = 0;
                                    montoPagar[i] = 0.0f;
                                    montoComision[i] = 0.0f;
                                    montoDeducido[i] = 0.0f;
                                    montoPagaCliente[i] = 0.0f;
                                    vuelto[i] = 0.0f;
                                    Console.WriteLine("La información ya fue eliminada.");
                                }
                                else if (respuesta2.ToString().ToUpper() == "N")
                                {
                                    Console.WriteLine("La información no fue eliminada.");
                                }
                                idEncontrado = true;
                                break;
                            }
                        }
                        if (!idEncontrado)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nN° de pago no encontrada...\n");
                            Console.ResetColor();
                        }
                        Console.WriteLine("\nDesea realizar otra consulta (S/N)");
                        while (!char.TryParse(Console.ReadLine(), out respuesta) || (respuesta = char.ToUpper(respuesta)) != 'S' && respuesta != 'N')
                        {
                            Console.Write("Por favor, ingrese 'S' o 'N': ");
                        }
                        Console.Clear();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite el formato correcto.");
                        continua = true;
                    }
                } while (continua);
            } while (respuesta != 'N');

        }
        //fin metodo eliminar
        #region METODOS REPORTES
        public static void reporteTodosPagos()
        {
            Console.Clear();
            Console.WriteLine("\t\tReporte todos los pagos\n");

            int totalRegistros = 0;
            float montoTotal = 0.0f;

            for (int i = 0; i < numPago.Length; i++)
            {
                // Verificar si el número de pago es mayor que 1 y menor o igual a 10
                if (numPago[i] >= 1 && numPago[i] <= 10)
                {
                    totalRegistros++;
                    montoTotal += montoPagaCliente[i];

                    Console.Clear();
                    Console.WriteLine($"\nDatos Encontrado Posicion Vector ({i}).\n");
                    Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                    Console.WriteLine($"\r\n  N° Pago: {numPago[i]}               ");
                    Console.WriteLine("\r\n+------------------------------------+");
                    Console.WriteLine($"\r\n  Fecha: {fecha[i]}  Hora: {hora[i]}  ");
                    Console.WriteLine($"\r\n  Cedula: {cedula[i]}                 ");
                    Console.WriteLine($"\r\n  Nombre Completo: {nombre[i]} {apellido1[i]} {apellido2[i]} ");
                    Console.WriteLine("\r\n+------------------------------------+");
                    Console.WriteLine($"\r\n  Monto Recibo: {montoPagaCliente[i]}   ");
                    Console.WriteLine("\r\n ==================================== \r\n\r\n");
                    Console.WriteLine("\t\t <PULSE ENTER PARA VER EL SIGUIENTE REPORT>");
                    Console.ReadLine();
                }
            }//fin for
            Console.Clear();
            Console.WriteLine($"Total de Registros: {totalRegistros}");
            Console.WriteLine($"Monto Total: {montoTotal}");
            Console.WriteLine("\t\t <PULSE CUALQUIER TECLA PARA ABANDONAR>");
            Console.ReadKey();
            Console.Clear();

        }//fin metodo todos los pagos
        public static void reporteTipoServicio(int idServicio)
        {
            respuesta = 'N';
            bool continua;
            int totalRegistros = 0;
            float montoTotal = 0.0f;
            do
            {
                continua = false;
                do
                {
                    try
                    {
                        idEncontrado = false;
                        Console.Clear();
                        Console.WriteLine("\n------------- Reporte Tipo de Servicio -------------");

                        Console.WriteLine("\nSeleccione codigo de Servicio:     [1] ELECTRICIDAD, [2] TELFONO, [3] AGUA:");
                        idServicio = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < tipoServicio.Length; i++)
                        {
                            if (idServicio.Equals(tipoServicio[i]))
                            {
                                totalRegistros++;
                                montoTotal += montoPagaCliente[i];
                                Console.Clear();
                                Console.WriteLine($"\nDatos Encontrado Posicion Vector ({i}).\n");
                                Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                                Console.WriteLine($"\r\n  N° Pago: {numPago[i]}               ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Fecha: {fecha[i]}  Hora: {hora[i]}  ");
                                Console.WriteLine($"\r\n  Cedula: {cedula[i]}                 ");
                                Console.WriteLine($"\r\n  Nombre Completo: {nombre[i]} {apellido1[i]} {apellido2[i]} ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Monto Recibo: {montoPagaCliente[i]}   ");
                                Console.WriteLine("\r\n ==================================== \r\n\r\n");
                                Console.WriteLine("\t\t <PULSE ENTER PARA VER EL SIGUIENTE REPORT>");
                                Console.ReadLine();
                                idEncontrado = true;
                                Console.Clear();
                            }
                        }

                        if (!idEncontrado)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nN° de Servicio no encontrada...");
                            Console.ResetColor();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite el formato correcto.");
                        continua = true;
                    }

                    Console.WriteLine($"\nTotal de Registros: {totalRegistros}");
                    Console.WriteLine($"Monto Total: {montoTotal}");
                    Console.WriteLine("\nDesea realizar otra consulta (S/N)");
                    while (!char.TryParse(Console.ReadLine(), out respuesta) || (respuesta = char.ToUpper(respuesta)) != 'S' && respuesta != 'N')
                    {
                        Console.WriteLine("Por favor, ingrese 'S' o 'N'.");
                    }
                    Console.Clear();
                } while (continua);
            } while (respuesta != 'N');
        }//fin metodo reporte tipo servicio
        public static void reporteCodigoCaja(int codCaja)
        {
            respuesta = 'N';
            bool continua;
            int totalRegistros = 0;
            float montoTotal = 0.0f;
            do
            {
                continua = false;
                do
                {
                    try
                    {
                        idEncontrado = false;
                        Console.Clear();
                        Console.WriteLine("\n------------- Reporte Codigo Caja -------------");

                        Console.WriteLine("\neleccione codigo de Servicio:     [1] CAJA #1, [2] CAJA #2, [3] CAJA #3:");
                        codCaja = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < numCaja.Length; i++)
                        {
                            if (codCaja.Equals(numCaja[i]))
                            {
                                totalRegistros++;
                                montoTotal += montoPagaCliente[i];
                                Console.Clear();
                                Console.WriteLine($"\nDatos Encontrado Posicion Vector ({i}).\n");
                                Console.WriteLine("\r\n ==================================== \r\n  Sistema Pago de Servicios Publicos  \r\n ====================================");
                                Console.WriteLine($"\r\n  N° Pago: {numPago[i]}               ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Fecha: {fecha[i]}  Hora: {hora[i]}  ");
                                Console.WriteLine($"\r\n  Cedula: {cedula[i]}                 ");
                                Console.WriteLine($"\r\n  Nombre Completo: {nombre[i]} {apellido1[i]} {apellido2[i]} ");
                                Console.WriteLine("\r\n+------------------------------------+");
                                Console.WriteLine($"\r\n  Monto Recibo: {montoPagaCliente[i]}   ");
                                Console.WriteLine("\r\n ==================================== \r\n\r\n");
                                Console.WriteLine("\t\t <PULSE ENTER PARA VER EL SIGUIENTE REPORT>");
                                Console.ReadLine();
                                idEncontrado = true;
                                Console.Clear();
                            }
                        }

                        if (!idEncontrado)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nN° de Servicio no encontrada...");
                            Console.ResetColor();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite el formato correcto.");
                        continua = true;
                    }

                    Console.WriteLine($"\nTotal de Registros: {totalRegistros}");
                    Console.WriteLine($"Monto Total: {montoTotal}");
                    Console.WriteLine("\nDesea realizar otra consulta (S/N)");
                    while (!char.TryParse(Console.ReadLine(), out respuesta) || (respuesta = char.ToUpper(respuesta)) != 'S' && respuesta != 'N')
                    {
                        Console.WriteLine("Por favor, ingrese 'S' o 'N'.");
                    }
                    Console.Clear();
                } while (continua);
            } while (respuesta != 'N');

        }//fin metodo reporte por codigo de caja
        public static void reporteDineroComisionado()
        {
            Console.Clear();
            Console.WriteLine("\t\tReporte Dinero Comisionado.\n");

            int totalRegistros = 0;
            float montoTotal = 0.0f;
            for (int i = 0; i < numPago.Length; i++)
            {
                totalComisionado = montoComision[i];
                if (numPago[i] >= 1 && numPago[i] <= 10)
                {
                    totalRegistros++;
                    montoTotal += totalComisionado;
                }
            }
            int contadorServicio1 = 0;
            int contadorServicio2 = 0;
            int contadorServicio3 = 0;

            // buscar en el arreglo tipo servicio y comparar los numeros en cada case del switch para luego ir sumando los registros a las variables contadores
            for (int i = 0; i < tipoServicio.Length; i++)
            {
                switch (tipoServicio[i])
                {
                    case 1:
                        contadorServicio1++;
                        break;
                    case 2:
                        contadorServicio2++;
                        break;
                    case 3:
                        contadorServicio3++;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("==========================================================================");
            Console.WriteLine($"Cantidad transacciones por tipo 1-Electricidad: {contadorServicio1}");
            Console.WriteLine($"Cantidad transacciones por tipo 2-Telefono: {contadorServicio2}");
            Console.WriteLine($"Cantidad transacciones por tipo 3-Agua: {contadorServicio3}");
            Console.WriteLine($"Total de Registros: {totalRegistros}");
            Console.WriteLine($"Monto Total Comisionado: {montoTotal}");
            Console.WriteLine("==========================================================================\n");
            Console.WriteLine("\t\t <PULSE CUALQUIER TECLA PARA ABANDONAR>");
            Console.ReadKey();
            Console.Clear();
        }//fin metodo reporte por dinero comisionado por servicios
        #endregion
    }//fin clase
}//fin namespace
