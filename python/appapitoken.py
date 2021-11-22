#!/usr/bin/env python3
import requests
import json
import datetime

class tokenGenerate:
    def __init__(self, url="", payload=""):
        self.url = url
        self.payload = payload
        self.headers = {"Content-Type": "application/x-www-form-urlencoded"}
        self.time_expired = datetime.datetime.now()
        self.expired = False
        self.token = {}
        self.json_data = {}

    def getToken(self):
        if self.time_expired <= datetime.datetime.now():
            response = requests.request("POST", self.url, headers=self.headers, data=self.payload)
            data = response.text
            self.json_data = json.loads(data)
            expiresin = self.json_data["expires_in"]
            self.token = data
            self.time_expired = datetime.datetime.now() + datetime.timedelta(0,(int(expiresin)-100)) 

        return {"Authorization": self.json_data["token_type"] + " " + self.json_data["access_token"]}
       

