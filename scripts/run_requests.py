import requests

API_BASE = r'https://localhost:5001/api'
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

session = requests.Session()


def register_users():
    print("Register Users")
    for user in USERS:
        r = session.post(REGISTER, json=user, verify=False)
        print(r.json())


def log_in(user):
    print("Log In")
    r = session.post(LOGIN, json=user, verify=False)
    session.headers['authorization'] = 'Bearer ' + r.json()['token']
    HEADERS['authorization'] = 'Bearer ' + r.json()['token']
    print(r.status_code)


def add_contacts():
    print("Add Contacts")
    r = session.post(CONTACTS, json={'userName': 'user2'}, headers=HEADERS, verify=False)
    print(r.request.headers)
    print(session.headers)
    print(r.text)


def list_contacts():
    print("List Contacts")
    r = session.get(CONTACTS, verify=False)
    print(r.text)


if __name__ == '__main__':
    register_users()
    log_in(USERS[0])
    add_contacts()
    list_contacts()