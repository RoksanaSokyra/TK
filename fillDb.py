#!/usr/bin/python3

import requests as r
import json
import random

host = "localhost"
port = "5000"

address = "http://" + host + ":" + port

class User():
    # TODO: add status code verification to all requests made
    def __init__(self, id):
        self.username = "user" + id
        self.password = "password" + id
        self.firstName = "firstName" + id
        self.lastName = "lastName" + id
        self.token = ""
        self.id = ""
        self.contactIds = []

    def register(self):
        registerJson = {
                "username": self.username,
                "password": self.password,
                "FirstName": self.firstName,
                "LastName": self.lastName
             }
        response = r.post(address + "/users/register", json=registerJson)

    def login(self):
        loginJson = {
                "username": self.username,
                "password": self.password
            }

        response = r.post(address + "/users/authenticate", json=loginJson)
        data = response.json()
        self.token = data["token"]
        self.id = str(data["id"])

    def addContact(self, user):
        if self.id == user.id or user.id in self.contactIds:
            return False
        else:
            response = r.post(address + "/users/" + self.id +  "/contacts",
                json=user.username)
            self.contactIds.append(user.id)
            user.contactIds.append(self.id)
            return True



if __name__ == "__main__":
    users = []
    n = 14
    start = 0
    for i in range(start, start + n):
        user = User(str(i))
        user.register()
        user.login()
        users.append(user)

    conections = 3

    print("Added ", len(users), "users")
    for user in users:
        while (len(user.contactIds) < conections):
            availableUsers = [u for u in users if len(u.contactIds) < conections]
            if len(availableUsers) == 0:
                break
            user.addContact(random.choice(availableUsers))
        print(user.id, " => ", user.contactIds)
