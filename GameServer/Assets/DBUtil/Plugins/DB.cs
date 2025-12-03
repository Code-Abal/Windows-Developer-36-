using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class DB : MonoBehaviour
{

    //회원가입화면에서 가입
    public static bool NewInsertMember(string id, string pwd)
    {
         if(NewMemberCheckIDMySql(id) == true)
         {
            if(NewMemberInsertMySql(id, pwd) == true)
            {
                print("값이 들어왔습니다");
                return true;
            }
            else if(NewMemberInsertMySql(id, pwd) == false)
            {
                print("mysql에러");
            }
         }
        
         return false;
    }

    //MySql에 회원가입 정보 저장하기.
    public static bool NewMemberInsertMySql(string UserID, string UserPWD)
    {
        try
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=Login;Uid=root;Pwd=root;");
            conn.Open();

            string insertQuery = string.Format("INSERT INTO userinfo(userID, userPassword) VALUES('{0}','{1}')", UserID, UserPWD);

            MySqlCommand command = new MySqlCommand(insertQuery, conn);
            //command.ExecuteNonQuery();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.Close();
                return true;
            }      
        }
        catch (Exception e)
        {
            print(e.Message);
        }

        return false;
    }
   
    //로그인 체크.
    public static bool LoginCheckMySql(string UserID, string UserPWD)
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=Login;Uid=root;Pwd=root;");
        conn.Open();

        string selectQuery = string.Format("Select * from userinfo where userID = '{0}' and userPassword = '{1}'",UserID, UserPWD);

        MySqlCommand command = new MySqlCommand(selectQuery, conn);
        MySqlDataReader userinfo = command.ExecuteReader();

        while (userinfo.Read())
        {
            if(UserID == (string)userinfo["userID"] && UserPWD == (string)userinfo["userPassword"])
            {
                conn.Close();
                print("로그인 정보 일치");
                return true;
            }
            else
            {
                break;
            }
        }

        print("로그인 정보 불일치");
        return false;
    }

    //중복아이디 체크.
    public static bool NewMemberCheckIDMySql(string NewUserID)
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=Login;Uid=root;Pwd=root;");
        conn.Open();

        string selectQuery = string.Format("Select * from userinfo where userID = '{0}'", NewUserID);

        MySqlCommand command = new MySqlCommand(selectQuery, conn);
        MySqlDataReader userinfo = command.ExecuteReader();

        while (userinfo.Read())
        {
            if (NewUserID == (string)userinfo["userID"])
            {
                Debug.Log("중복검사 통과실패");
                conn.Close();
                return false;
            }
            else
            {
                break;
            }
        }
        Debug.Log("중복검사 통과성공");
        return true;
    }
}
