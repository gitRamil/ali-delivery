import React, { useState, useEffect } from 'react';
import './App.css';
import useSignalR from './hooks/useSignalR';
import LocationMap from './components/LocationMap';
import LocationInfo from './components/LocationInfo';
import LocationHistory from './components/LocationHistory';

const SIGNALR_HUB_URL = 'http://localhost:5091/locationHub'; // Замените на ваш URL

function App() {
    const [locations, setLocations] = useState([]);
    const [currentLocation, setCurrentLocation] = useState(null);

    const {
        connectionState,
        isSubscribed,
        subscribeToLocations,
        unsubscribeFromLocations,
        onLocationUpdate
    } = useSignalR(SIGNALR_HUB_URL);

    // Обработка обновлений локаций
    useEffect(() => {
        onLocationUpdate((locationData) => {
            setCurrentLocation(locationData);
            setLocations(prev => [...prev, locationData].slice(-50)); // Храним последние 50 локаций
        });
    }, [onLocationUpdate]);

    return (
        <div style={{ 
            padding: '20px', 
            fontFamily: 'Arial, sans-serif',
            maxWidth: '1200px',
            margin: '0 auto'
        }}>
            <header style={{ marginBottom: '30px', textAlign: 'center' }}>
                <h1 style={{ 
                    color: '#333',
                    marginBottom: '10px',
                    fontSize: '2.5em'
                }}>
                    🗺️ Отслеживание локаций в реальном времени
                </h1>
                <p style={{ color: '#666', fontSize: '1.1em' }}>
                    Мониторинг GPS координат через SignalR и React
                </p>
            </header>

            {/* Информация о подключении и кнопки управления */}
            <div style={{ 
                display: 'flex', 
                gap: '15px', 
                marginBottom: '20px',
                flexWrap: 'wrap',
                alignItems: 'center',
                justifyContent: 'center'
            }}>
                <button
                    onClick={subscribeToLocations}
                    disabled={isSubscribed || connectionState !== 'Connected'}
                    style={{
                        padding: '10px 20px',
                        backgroundColor: isSubscribed ? '#6c757d' : '#28a745',
                        color: 'white',
                        border: 'none',
                        borderRadius: '5px',
                        cursor: isSubscribed || connectionState !== 'Connected' ? 'not-allowed' : 'pointer',
                        fontSize: '16px'
                    }}
                >
                    {isSubscribed ? '✅ Подписан на локации' : '🔔 Подписаться на локации'}
                </button>
                
                <button
                    onClick={unsubscribeFromLocations}
                    disabled={!isSubscribed || connectionState !== 'Connected'}
                    style={{
                        padding: '10px 20px',
                        backgroundColor: !isSubscribed ? '#6c757d' : '#dc3545',
                        color: 'white',
                        border: 'none',
                        borderRadius: '5px',
                        cursor: !isSubscribed || connectionState !== 'Connected' ? 'not-allowed' : 'pointer',
                        fontSize: '16px'
                    }}
                >
                    🔕 Отписаться от локаций
                </button>

                <div style={{ 
                    padding: '8px 16px',
                    backgroundColor: '#f8f9fa',
                    borderRadius: '20px',
                    border: '1px solid #ddd',
                    fontSize: '14px'
                }}>
                    📊 Локаций получено: <strong>{locations.length}</strong>
                </div>
            </div>

            {/* Основной контент */}
            <div style={{
                display: 'grid',
                gridTemplateColumns: '1fr 1fr',
                gap: '20px',
                marginBottom: '30px'
            }}>
                {/* Левая колонка - карта */}
                <div>
                    <h2 style={{ marginBottom: '15px', color: '#333' }}>🗺️ Карта локаций</h2>
                    <LocationMap 
                        locations={locations} 
                        currentLocation={currentLocation} 
                    />
                </div>

                {/* Правая колонка - информация */}
                <div>
                    <LocationInfo 
                        currentLocation={currentLocation}
                        connectionState={connectionState}
                        isSubscribed={isSubscribed}
                    />
                </div>
            </div>

            {/* История локаций на всю ширину */}
            <LocationHistory locations={locations} />
        </div>
    );
}

export default App;
