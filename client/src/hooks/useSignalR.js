import { useEffect, useCallback, useState } from 'react';
import signalRService from '../services/signalRService';

const useSignalR = (hubUrl) => {
    const [connectionState, setConnectionState] = useState('Disconnected');
    const [isSubscribed, setIsSubscribed] = useState(false);

    // Подключение к SignalR
    useEffect(() => {
        const connectToHub = async () => {
            try {
                signalRService.createConnection(hubUrl);
                
                // Обработчики состояния подключения
                signalRService.connection.onreconnecting(() => {
                    setConnectionState('Reconnecting');
                });

                signalRService.connection.onreconnected(() => {
                    setConnectionState('Connected');
                    signalRService.setupEventListeners();
                });

                signalRService.connection.onclose(() => {
                    setConnectionState('Disconnected');
                    setIsSubscribed(false);
                });

                const connected = await signalRService.startConnection();
                if (connected) {
                    setConnectionState('Connected');
                } else {
                    setConnectionState('Failed');
                }
            } catch (error) {
                console.error('Connection failed:', error);
                setConnectionState('Failed');
            }
        };

        connectToHub();

        // Cleanup при размонтировании
        return () => {
            signalRService.stopConnection();
        };
    }, [hubUrl]);

    // Подписка на локации
    const subscribeToLocations = useCallback(async () => {
        try {
            await signalRService.invoke('SubscribeToAllLocations');
            setIsSubscribed(true);
        } catch (error) {
            console.error('Failed to subscribe to locations:', error);
        }
    }, []);

    // Отписка от локаций
    const unsubscribeFromLocations = useCallback(async () => {
        try {
            await signalRService.invoke('UnsubscribeFromAllLocations');
            setIsSubscribed(false);
        } catch (error) {
            console.error('Failed to unsubscribe from locations:', error);
        }
    }, []);

    // Обработчик событий SignalR
    const onLocationUpdate = useCallback((callback) => {
        signalRService.on('LocationUpdated', callback);
        signalRService.on('SubscribedToAllLocations', () => setIsSubscribed(true));
        signalRService.on('UnsubscribedFromAllLocations', () => setIsSubscribed(false));
    }, []);

    return {
        connectionState,
        isSubscribed,
        subscribeToLocations,
        unsubscribeFromLocations,
        onLocationUpdate
    };
};

export default useSignalR;
