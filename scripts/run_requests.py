import requests

API_BASE = r'http://localhost:5000/api'
AUTHENTICATE_BASE = API_BASE + '/authenticate'
REGISTER = AUTHENTICATE_BASE + '/register'
LOGIN = AUTHENTICATE_BASE + '/login'
CONTACTS = API_BASE + '/user/contacts'
HEADERS = {}
USERS = [
    {
        'firstName': 'John',
        'lastName': 'Doe',
        'email': 'user1@test.com',
        'username': 'user1',
        'password': 'P@ssw0rd123'
    },
    {
        'firstName': 'John',
        'lastName': 'Doe',
        'email': 'user1@test.com',
        'username': 'user2',
        'password': 'P@ssw0rd123'
    }
]


def register_users():
    for user in USERS:
        r = requests.post(REGISTER, json=user, verify=False)
        print(r.json())


def log_in(user):
    r = requests.post(LOGIN, json=user, verify=False)
    token = r.json()['token']
    HEADERS['Authorization'] = 'Bearer ' + token


def add_contacts():
    r = requests.post(CONTACTS, json={'userName': 'user2'}, headers=HEADERS, verify=False)
    print(r.text)


def list_contacts():
    r = requests.get(CONTACTS, headers=HEADERS, verify=False)
    print(r.text)


if __name__ == '__main__':
    register_users()
    log_in(USERS[0])
    add_contacts()
    list_contacts()