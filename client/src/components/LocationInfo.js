import React from 'react';

const LocationInfo = ({ currentLocation, connectionState, isSubscribed }) => {
    const getConnectionColor = () => {
        switch (connectionState) {
            case 'Connected': return '#28a745';
            case 'Reconnecting': return '#ffc107';
            case 'Disconnected': return '#6c757d';
            case 'Failed': return '#dc3545';
            default: return '#6c757d';
        }
    };

    return (
        <div style={{ marginBottom: '20px' }}>
            {/* Статус подключения */}
            <div style={{
                padding: '12px',
                borderRadius: '8px',
                backgroundColor: '#f8f9fa',
                border: `2px solid ${getConnectionColor()}`,
                marginBottom: '15px'
            }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <div style={{
                        width: '12px',
                        height: '12px',
                        borderRadius: '50%',
                        backgroundColor: getConnectionColor()
                    }} />
                    <span style={{ fontWeight: 'bold' }}>
                        Состояние подключения: {connectionState}
                    </span>
                </div>
                <div style={{ marginTop: '5px', fontSize: '14px', color: '#6c757d' }}>
                    Подписка на локации: {isSubscribed ? '✅ Активна' : '❌ Неактивна'}
                </div>
            </div>

            {/* Информация о последней локации */}
            <div style={{
                padding: '15px',
                backgroundColor: '#f0f0f0',
                borderRadius: '8px',
                border: '1px solid #ddd'
            }}>
                <h3 style={{ margin: '0 0 10px 0', color: '#333' }}>Последняя локация:</h3>
                {currentLocation ? (
                    <div>
                        <div style={{ marginBottom: '8px' }}>
                            <strong>ID:</strong> {currentLocation.id.substring(0, 8)}...
                        </div>
                        <div style={{ marginBottom: '8px' }}>
                            <strong>Координаты:</strong> {currentLocation.latitude.toFixed(6)}, {currentLocation.longitude.toFixed(6)}
                        </div>
                        <div style={{ marginBottom: '8px' }}>
                            <strong>Время создания:</strong> {new Date(currentLocation.createdAt).toLocaleString()}
                        </div>
                        <div>
                            <strong>Время получения:</strong> {new Date(currentLocation.timestamp).toLocaleString()}
                        </div>
                    </div>
                ) : (
                    <p style={{ color: '#6c757d', margin: 0 }}>Ожидание данных...</p>
                )}
            </div>
        </div>
    );
};

export default LocationInfo;
