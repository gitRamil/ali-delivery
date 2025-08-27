import * as signalR from '@microsoft/signalr';

class SignalRService {
    constructor() {
        this.connection = null;
        this.listeners = new Map();
    }

    // Создание подключения
    createConnection(hubUrl) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .configureLogging(signalR.LogLevel.None) // Отключаем логи
            .withAutomaticReconnect()
            .build();

        return this.connection;
    }

    // Запуск подключения
    async startConnection() {
        if (!this.connection) {
            throw new Error('Connection not created. Call createConnection first.');
        }

        try {
            await this.connection.start();
            return true;
        } catch (error) {
            console.error('SignalR Connection Error:', error);
            return false;
        }
    }

    // Остановка подключения
    async stopConnection() {
        if (this.connection) {
            await this.connection.stop();
        }
    }

    // Подписка на событие
    on(eventName, callback) {
        if (this.connection) {
            this.connection.on(eventName, callback);
        }
        
        // Сохраняем колбэк для повторного подключения
        if (!this.listeners.has(eventName)) {
            this.listeners.set(eventName, []);
        }
        this.listeners.get(eventName).push(callback);
    }

    // Отписка от события
    off(eventName, callback) {
        if (this.connection) {
            this.connection.off(eventName, callback);
        }
    }

    // Вызов метода на сервере
    async invoke(methodName, ...args) {
        if (this.connection) {
            try {
                return await this.connection.invoke(methodName, ...args);
            } catch (error) {
                console.error(`Error invoking ${methodName}:`, error);
                throw error;
            }
        }
    }

    // Получение состояния подключения
    getConnectionState() {
        return this.connection ? this.connection.state : 'Disconnected';
    }

    // Переустановка обработчиков после переподключения
    setupEventListeners() {
        if (this.connection) {
            this.listeners.forEach((callbacks, eventName) => {
                callbacks.forEach(callback => {
                    this.connection.on(eventName, callback);
                });
            });
        }
    }
}

// Создаем именованный экземпляр и экспортируем его
const signalRServiceInstance = new SignalRService();
export default signalRServiceInstance;
