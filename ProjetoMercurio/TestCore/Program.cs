using System;
using ProjetoMercurioCore;
using ProjetoMercurioCore.Model;
using ProjetoMercurioCore.db.DataManipulation;
using ProjetoMercurioCore.Model.Enum;
using MercurioCore.Model.Exceptions;
using System.Collections.Generic;

namespace TestCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Sensor inicial = new Sensor(1);
                Sensor final = new Sensor(6);

                Rota rota = new Rota(inicial, final);
                rota.CreateItem();
                Console.WriteLine(rota.Tracado);
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
