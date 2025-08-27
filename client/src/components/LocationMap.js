import React, { useEffect, useRef } from 'react';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';

// Исправляем проблему с иконками маркеров в Leaflet
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});

const LocationMap = ({ locations, currentLocation }) => {
    const mapRef = useRef(null);
    const mapInstanceRef = useRef(null);
    const markersRef = useRef([]);

    // Инициализация карты
    useEffect(() => {
        if (mapRef.current && !mapInstanceRef.current) {
            mapInstanceRef.current = L.map(mapRef.current).setView([55.8304, 49.0661], 13);
            
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors'
            }).addTo(mapInstanceRef.current);
        }

        return () => {
            if (mapInstanceRef.current) {
                mapInstanceRef.current.remove();
                mapInstanceRef.current = null;
            }
        };
    }, []);

    // Обновление маркеров
    useEffect(() => {
        if (!mapInstanceRef.current) return;

        // Очищаем старые маркеры
        markersRef.current.forEach(marker => {
            mapInstanceRef.current.removeLayer(marker);
        });
        markersRef.current = [];

        // Добавляем новые маркеры
        locations.slice(-20).forEach((location, index) => { // Показываем последние 20
            const marker = L.marker([location.latitude, location.longitude])
                .bindPopup(`
                    <div>
                        <b>Локация ${location.id.substring(0, 8)}...</b><br>
                        <strong>Время:</strong> ${new Date(location.createdAt).toLocaleString()}<br>
                        <strong>Координаты:</strong> ${location.latitude.toFixed(6)}, ${location.longitude.toFixed(6)}
                    </div>
                `)
                .addTo(mapInstanceRef.current);

            markersRef.current.push(marker);
        });
    }, [locations]);

    // Центрирование на последней локации
    useEffect(() => {
        if (currentLocation && mapInstanceRef.current) {
            mapInstanceRef.current.setView([currentLocation.latitude, currentLocation.longitude], 15);
        }
    }, [currentLocation]);

    return (
        <div 
            ref={mapRef} 
            style={{ 
                height: '400px', 
                width: '100%', 
                border: '1px solid #ccc',
                borderRadius: '8px'
            }} 
        />
    );
};

export default LocationMap;
