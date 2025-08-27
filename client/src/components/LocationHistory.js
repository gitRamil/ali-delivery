import React from 'react';

const LocationHistory = ({ locations }) => {
    if (locations.length === 0) {
        return (
            <div style={{
                padding: '20px',
                textAlign: 'center',
                color: '#6c757d',
                border: '1px solid #ddd',
                borderRadius: '8px',
                backgroundColor: '#f8f9fa'
            }}>
                История локаций пуста
            </div>
        );
    }

    return (
        <div>
            <h3 style={{ marginBottom: '15px', color: '#333' }}>История локаций:</h3>
            <div style={{
                maxHeight: '300px',
                overflowY: 'auto',
                border: '1px solid #ddd',
                borderRadius: '8px',
                backgroundColor: '#fff'
            }}>
                {locations.slice().reverse().map((location, index) => (
                    <div
                        key={location.id}
                        style={{
                            padding: '12px 15px',
                            borderBottom: index < locations.length - 1 ? '1px solid #eee' : 'none',
                            backgroundColor: index === 0 ? '#e8f5e8' : 'transparent'
                        }}
                    >
                        <div style={{ fontWeight: 'bold', marginBottom: '5px' }}>
                            {new Date(location.createdAt).toLocaleString()}
                        </div>
                        <div style={{ fontSize: '14px', color: '#666' }}>
                            <div>ID: {location.id.substring(0, 8)}...</div>
                            <div>Координаты: {location.latitude.toFixed(4)}, {location.longitude.toFixed(4)}</div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default LocationHistory;
