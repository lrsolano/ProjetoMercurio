using System;
using ProjetoMercurioCore;
using ProjetoMercurioCore.Model;
using ProjetoMercurioCore.db.DataManipulation;
using ProjetoMercurioCore.Model.Enum;
using MercurioCore.Model.Exceptions;

namespace TestCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Passei aqui");
            try
            {
                string nomeSensor = "Sensor 6";
                bool inicial = false;
                Sensor sensorAnterior = new Sensor(4);
                Console.WriteLine("Criei o Sensor");
                Direcao direcao = new Direcao(1);
                Console.WriteLine("Criei a Direção");
                DirecaoRota direcaoRota = DirecaoRota.Ida;


                Sensor sensor = new Sensor(nomeSensor, inicial, sensorAnterior, direcao, direcaoRota);
                sensor.CreateSensor();


                Console.WriteLine(string.Format("ID do Sensor: {0}", sensor.Id));
            }
            catch (DBConnectionException ex)
            {
                Console.WriteLine(ex);
            }
            catch(MercurioCoreException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
