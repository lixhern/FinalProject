var arrLang = {
    'en': {
        'home': 'Home page',
        'hello': 'Welcome, ',
        'logout': 'Logout',
        'signin': 'Registration',
        'login': 'Log in',
        'dark': 'Dark',
        'light': 'Light',
        'search': 'Search',
        'language': 'Language',
        'welcome1': 'Welcome, ',
        'welcome2': 'Welcome!',
        'collections': 'Five of the biggest collections',
        'nameCol': 'Collection name',
        'catCol': 'Categories',
        'author': 'Author',
        'items': 'The five most recently added aitems',
        'nameIte': 'Item name',
        'tags': 'Top 30 tags',
        'mycollections': 'My collections',
        'nocol': "You don't have any collections yet",
        'personal': "Personal account",
        'createcol': 'Create new collection?',
        'admin': 'ADMIN',
        'user': 'User',
        'allcol': 'All collections',
        'roles': 'Role assignment',
        'email': 'Email address',
        'password': 'Password',
        'submit': 'Submit',
        'login2': 'Create account?',
        'username': 'User name',
        'signin2': 'Already have an account?',
        'listRoles': 'Role list',
        'id': 'Id',
        'status': 'Status',
        'takeadmin': 'Take away administrator rights',
        'giveadmin': 'Give administrator rights',
        'block': 'Block',
        'unblock': 'Unblock',
        'delete': 'Delete',
        'imageUrl': 'Image URL',
        'description': 'Description',
        'select': 'Select category',
        'cfield1': 'Name of the first custom field',
        'cfield2': 'Name of the second custom field',
        'cfield3': 'Name of the third custom field',
        'add': 'Add custom field',
        'delc': 'Delete custom field',
        'nameItem': 'Name of the Item',
        'tags2': 'Tags',
        'createitem': 'Create Item',
        'authorI': 'Author of the item:',
        'nameI': 'Name of collection:',
        'col1': 'Collection:',
        'col2': 'Collections:',
        'category': 'Category:',
        'items2': 'Items:',
        'uscol': 'Users collections',
        'usite': 'Users items',
        'itemtag': 'Items by tag:',
        'colbycat': 'Collections by category:',
        'category1': 'Category',
        'priority': 'Priority',
        'theme': 'Theme',
        'high': 'High',
        'error': 'Error',
        'issueOk': 'Issue created',
        'crIssue': 'Create Issue',
        'created': 'Your issue has been created. You can view it',
        'here': 'here',
        'allIssues': 'See all issues',
        'createIssue': 'Create a request to the support team'
    },
    'ru': {
        'home': 'Домашняя страница',
        'hello': 'Привет, ',
        'logout': 'Выход',
        'signin': 'Зарегистрироваться',
        'login': 'Войти',
        'dark': 'Тёмная',
        'light': 'Светлая',
        'search': 'Поиск',
        'language': 'Язык',
        'welcome1': 'Добро пожаловать, ',
        'welcome2': 'Добро пожаловать!',
        'collections': 'Пять самых больших коллекций',
        'nameCol': 'Название коллекции',
        'catCol': 'Категории',
        'author': 'Автор',
        'items': 'Пять последних добавленных айтемов',
        'nameIte': 'Название айтема',
        'tags': 'Топ 30 тегов',
        'mycollections': 'Мои коллекции',
        'nocol': "У вас пока нет ни одной коллекции.",
        'personal': "Личный кабинет",
        'createcol': 'Создать новую коллекцию?',
        'admin': 'АДМИНИСТРАТОР',
        'user': 'Пользователь',
        'allcol': 'Все коллекции',
        'roles': 'Назначение ролей',
        'email': 'Адресс электронной почты',
        'password': 'Пароль',
        'submit': 'Подтвердить',
        'login2': 'Создать аккаунт?',
        'username': 'Имя пользователя',
        'signin2': 'Уже есть аккаунт?',
        'listRoles': 'Список ролей',
        'id': 'Идентификатор',
        'status': 'Статус',
        'takeadmin': 'Забрать права администратора',
        'giveadmin': 'Выдать права администратора',
        'block': 'Заблокировать',
        'unblock': 'Разблокировать',
        'delete': 'Удалить',
        'imageUrl': 'Image URL',
        'description': 'Описание',
        'select': 'Выбор категории',
        'cfield1': 'Название первого кастомного поля',
        'cfield2': 'Название второго кастомного поля',
        'cfield3': 'Название третьего кастомного поля',
        'add': 'Добавить кастомное поле',
        'delc': 'Удалить кастомное поле',
        'nameItem': 'Название айтема',
        'tags2': 'Тэги',
        'createitem': 'Создать айтем',
        'authorI': 'Автор айтема:',
        'nameI': 'Название коллекции:',
        'col1': 'Коллекция:',
        'col2': 'Коллекции:',
        'category': 'Категория:',
        'items2': 'Айтемы:',
        'uscol': 'Коллекции пользователя',
        'usite': 'Айтемы пользователя',
        'itemtag': 'Айтемы с тегом:',
        'colbycat': 'Коллекции с категорией: ',
        'category1': 'Категория',
        'priority': 'Приоритет',
        'theme': 'Тема',
        'high': 'Высокий',
        'error': 'Ошибка',
        'issueOk': 'Заявка создана',
        'crIssue': 'Создать заявку',
        'created': 'Ваша заявка создана. Вы можете просмореть её ',
        'here': 'здесь',
        'allIssues': 'Просмотреть все обращения в службу поддержки',
        'createIssue': 'Создать заявку в службу поддержки'
    }
}


var translate = document.querySelectorAll('.translate')
var elements = document.querySelectorAll('.lang')

var currentLang = localStorage.getItem('lang') ? localStorage.getItem('lang') : document.documentElement.getAttribute('lang')

var setLang = lang => {
    document.documentElement.setAttribute('lang', lang)
    localStorage.setItem('lang', lang)
    setActive(lang);
    setLanguage(lang);
    
};

setLang(currentLang)

function setLanguage(lang) {
    elements.forEach(function (item) {
        var key = item.getAttribute('key')
        item.textContent = arrLang[lang][key]
    })
}

function setActive(lang) {
    translate.forEach(function (item) {
        if (item.id === lang) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    })
}    

translate.forEach(t => {
    t.addEventListener('click', function () {
        var lang = this.id;
        setLang(lang); 
        
    }); 
});


