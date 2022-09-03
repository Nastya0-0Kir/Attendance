// See https://aka.ms/new-console-template for more information

//Спроектируйте систему учета посещаемости студентов по датам (в консольном приложении)
//1.При вводе в консоль команды “статистика” программа должна выводить информации о посещаемости
//1.1. Создать таблицу: “Посещаемость” (id, ФИО студента, дата)
//1.2.Заполнить эту таблицу тестовыми данными
//ДЗ:
//2.Добавьте возможность добавления новых студентов через консоль
//3. Добавьте возможность указывать инфо о посещаемости для студента по дате


using Microsoft.Data.Sqlite;
using System.Data;
using System.Runtime.InteropServices;

var createDataBase = "Data Source = Students.db";

using (var connection = new SqliteConnection(createDataBase))
{
    connection.Open();
    while (true) 
    {
        Console.WriteLine("Add Students: enter 1\n " +
                          "See list: enter \"statistics\"\n" +
                          "Exit: 0");
        var enter = Console.ReadLine();
        if (enter == "1")
        {
            Console.WriteLine("Enter FIO: ");
            var nameStudents = Console.ReadLine();
            Console.WriteLine("Enter date: ");
            var dateOfVisit = Console.ReadLine();
            StudentsAdd(connection, nameStudents, dateOfVisit);
        }

        if (enter == "statistics")
        {
            ViewStatistics(connection);
            var command = connection.CreateCommand();
        }

        if(enter == "0")
        {
            break;
        }
    }
}


void StudentsAdd(IDbConnection connection, string name, string date)
{
    using var command = connection.CreateCommand();
    command.CommandText = @$"insert into Attendance(FIO,Date)
                              values ('{name}', '{date}')";
    command.ExecuteNonQuery();
}

void ViewStatistics(IDbConnection connection)
{
    using var command = connection.CreateCommand();
    command.CommandText = "select *from Attendance";
    using (IDataReader reader = command.ExecuteReader())

            while (reader.Read())
            {
                var id = reader["Id"];
                var name=reader["FIO"];
                var date=reader["Date"];
                Console.WriteLine($"{id} {name} {date}");
            }
}
