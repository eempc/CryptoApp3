using CryptoAddress.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace CryptoAddress.Data {
    class UserAddressDatabase {
        // Get the full path of where the database file will be kept. This will work on both Windows emulator and on the phone
        private static string fileName = "UserAddresses007.db";
        private static string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string databasePath = Path.Combine(personalFolder, fileName);

        // Create the database file if it does not exist and seed it with an address
        public static void CreateDatabase() {
            if (!File.Exists(databasePath)) {
                SQLiteConnection db = new SQLiteConnection(databasePath);
                db.CreateTable<UserAddress>();

                UserAddress seedingAddress = new UserAddress() {
                    Id = 1,
                    Name = "My first Bitcoin wallet",
                    Address = "00112233445566778899aabbccddeeff",
                    CryptoSymbol = "BTC",                   
                };

                db.Insert(seedingAddress);

                UserAddress seedingAddress2 = new UserAddress() {
                    Id = 2,
                    Name = "My first Ethereum wallet",
                    Address = "0x888888888888888",
                    CryptoSymbol = "ETH",                   
                };

                db.Insert(seedingAddress2);

                db.Close();
            }
        }

        // Create or update
        public static async void Save(UserAddress address) {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(databasePath);
            if (!address.Id.HasValue || address.Id <= 0) {
                await db.InsertAsync(address);
            } else {
                await db.UpdateAsync(address);
            }
            await db.CloseAsync();
        }

        // Read all into a list
        public static List<UserAddress> GetAllUserAddresses() {
            List<UserAddress> list = new List<UserAddress>();
            SQLiteConnection db = new SQLiteConnection(databasePath);
            TableQuery<UserAddress> table = db.Table<UserAddress>();

            foreach (UserAddress item in table) {
                list.Add(item);
            }
            db.Close();
            return list;
        }

        // Get a single entry
        public static UserAddress GetUserAddressById(int number) {
            // Normal retrieval method
            SQLiteConnection db = new SQLiteConnection(databasePath);
            UserAddress singleAddress = db.Table<UserAddress>().Where(address => address.Id == number).FirstOrDefault();
            db.Close();            
            return singleAddress;
        }

        // Delete
        public static async void Delete(int id) {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(databasePath);
            await db.DeleteAsync<UserAddress>(id);
            await db.CloseAsync();
        }
    }
}
