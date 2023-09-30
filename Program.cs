using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lab1_n2_2 {
  public class Program {
    public static List<ProfileStudent> profileStudentList = new List<ProfileStudent>();
    public static string pathJsonFile = Path.Combine(Environment.CurrentDirectory, @"profiles.json");
    public const int CountSchool = 5;

    static void Main() {
      Console.Write("1. Сгенерировать новые анкеты и высчитать лучших по сумме\n" +
        "2. Считать анкеты с Json-файла и высчитать лучших по сумме\n"+
        "3. Добавить школьника\n" +
        "4. Очистить консоль\n\n\tВыберите желаемый пункт: ");
      int userChoice;
      int.TryParse(Console.ReadLine(), out userChoice);

      switch (userChoice) {
        case 1:
          Task1();
          
          break;
        case 2:
          if (!Task2()) {
            Console.WriteLine("Файл пустой !!! Сначала заполните его анкетами !!!");
          }
          
          break;
        case 3:
          Task3();

          break;
        case 4:
          Console.Clear();

          break;
        default:
          Console.WriteLine("Такого пункта не существует !!! Попробуйте ещё раз");
          
          break;
      }

      Main();
    }

    public static void Task1() {
      Console.Write("Введите количество желаемых анкет: ");
      int countProfiles;
      if (!int.TryParse(Console.ReadLine(), out countProfiles)) {
        Console.WriteLine("Неверный формат !!! Введите число !!!");
        Task1();

        return;
      }

      for (int indexProfile = 0; indexProfile < countProfiles; ++indexProfile) {
        profileStudentList.Add(RandomData.setRandomValues());
      }

      ToJsonFile();

      Console.WriteLine("Анкеты заполнены !!!");

      for (int indexProfile = 0; indexProfile < countProfiles; ++indexProfile) {
        Console.WriteLine(profileStudentList[indexProfile]);
      }

      BestInTotal();
    }

    public static bool Task2() {
      if (!FromJsonFile()) {
        return false;
      }

      Console.WriteLine("Json-файл считан !!!");

      foreach (var profileStudent in profileStudentList) {
        Console.WriteLine(profileStudent);
      }

      BestInTotal();

      return true;
    }

    private static void BestInTotal() {
      ProfileStudent[] bestStudent = new ProfileStudent[CountSchool];
      for (int indexStudent = 0; indexStudent < bestStudent.Length; ++indexStudent) {
        bestStudent[indexStudent] = new ProfileStudent();
      }

      for (int indexSchool = 0; indexSchool < bestStudent.Length; ++indexSchool) {
        foreach (ProfileStudent student in profileStudentList) {
          if ((student.GradeLit + student.GradeMath) >
            (bestStudent[indexSchool].GradeLit + bestStudent[indexSchool].GradeMath) && 
            (student.SchoolNumber == indexSchool + 1)) {
            bestStudent[indexSchool] = student;
          }
        }
      }

      for (int indexSchool = 0; indexSchool < bestStudent.Length; ++indexSchool) {
        if (bestStudent[indexSchool].SchoolNumber == default) {
          Console.WriteLine($"Школа №{indexSchool + 1}:\nНет анкет студентов из этой школы!\n");
        } else {
          Console.WriteLine($"Школа №{indexSchool + 1}:\n{bestStudent[indexSchool]}");
        }
      }
    }

    private static void Task3() {
      FromJsonFile();

      ProfileStudent student = new ProfileStudent();

      Console.Write("Введите фамилию: ");
      student.LastName = Console.ReadLine();
      if (student.LastName == string.Empty) {
        Console.WriteLine("ВЫ не ввели фамилию !!!");

        Task3();
      }

      Console.Write("Введите имя: ");
      student.FirstName = Console.ReadLine();
      if (student.FirstName == string.Empty) {
        Console.WriteLine("ВЫ не ввели имя !!!");

        Task3();
      }

      Console.Write("Введите номер школы: ");
      if (!int.TryParse(Console.ReadLine(), out int schoolNumber)) {
        Console.WriteLine("Номер школы должен быть числом !!!");

        Task3();

        return;
      }
      try {
        student.SchoolNumber = schoolNumber; 
      } catch (Exception ex){
        Console.WriteLine(ex.Message);
        Console.WriteLine("Попробуйте ещё раз !!!\n");

        Task3();

        return;
      }

      Console.Write("Введите оценку по математике: ");
      if (!int.TryParse(Console.ReadLine(), out int gradeMath)) {
        Console.WriteLine("Оценка должна быть числом !!!");

        Task3(); 
        
        return;
      }
      student.GradeMath = schoolNumber;

      Console.Write("Введите оценку по литературе: ");
      if (!int.TryParse(Console.ReadLine(), out int gradeLit)) {
        Console.WriteLine("Оценка должна быть числом !!!");

        Task3(); 
        
        return;
      }
      student.GradeLit = gradeLit;

      profileStudentList.Add(student);

      ToJsonFile();
    }

    private static bool FromJsonFile() {
      string profilesStudentsText;

      profilesStudentsText = File.ReadAllText(pathJsonFile);

      JArray profilesStudentsJson = JsonConvert.DeserializeObject<JArray>(profilesStudentsText);

      if (profilesStudentsJson == null) {
        return false;
      }

      foreach (JObject profileStudentJson in profilesStudentsJson) {
        profileStudentList.Add(new ProfileStudent() {
          FirstName = profileStudentJson["first_name"].ToString(),
          LastName = profileStudentJson["last_name"].ToString(),
          SchoolNumber = int.Parse(profileStudentJson["school_number"].ToString()),
          GradeLit = int.Parse(profileStudentJson["grade_lit"].ToString()),
          GradeMath = int.Parse(profileStudentJson["grade_math"].ToString()),
        });
      }

      return true;
    }

    private static void ToJsonFile() {
      JArray profilesStudentsJson = new JArray();

      foreach (var profileStudent in profileStudentList) {
        JObject profileStudentJson = new JObject();

        profileStudentJson["last_name"] = profileStudent.LastName;
        profileStudentJson["first_name"] = profileStudent.FirstName;
        profileStudentJson["school_number"] = profileStudent.SchoolNumber;
        profileStudentJson["grade_math"] = profileStudent.GradeMath;
        profileStudentJson["grade_lit"] = profileStudent.GradeLit;

        profilesStudentsJson.Add(profileStudentJson);
      }

      File.WriteAllText(pathJsonFile, profilesStudentsJson.ToString());
    }
  }
}
