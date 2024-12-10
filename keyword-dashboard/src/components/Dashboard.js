import React, { useState, useEffect } from "react";

const Dashboard = () => {
  const [keywords, setKeywords] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("https://localhost:7147/api/Keywords/GetKeywords")
      .then((response) => response.json())
      .then((data) => {
        setKeywords(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
        setLoading(false);
      });
  }, []);

  const totalKeywords = keywords.length;
  const totalVolume = keywords.reduce((acc, keyword) => acc + keyword.SearchVolume, 0);

  return (
    <div className="min-h-screen bg-gray-200 p-6 space-y-6"> {/* Cambié aquí bg-gray-50 a bg-gray-200 */}
      <div className="bg-white p-6 rounded-lg shadow-lg flex items-center justify-between space-x-6">
        <div className="bg-gradient-to-r from-teal-400 to-teal-500 text-white p-4 rounded-lg shadow-lg flex flex-col items-center">
          <span className="text-lg font-bold">Volumen Total de Búsqueda</span>
          <span className="text-3xl font-bold mt-2">{totalVolume}</span>
        </div>
        <div className="bg-gradient-to-r from-teal-400 to-teal-500 text-white p-4 rounded-lg shadow-lg flex flex-col items-center">
          <span className="text-lg font-bold">Keywords</span>
          <span className="text-3xl font-bold mt-2">{totalKeywords}</span>
        </div>
      </div>

      {loading ? (
        <div className="text-center">
          <p className="text-xl text-gray-500">Cargando...</p>
        </div>
      ) : (
        <div className="overflow-x-auto bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold text-gray-800">Lista de Keywords</h3>
          <table className="min-w-full mt-4 table-auto">
            <thead className="bg-gray-100">
              <tr>
                <th className="px-6 py-3 text-left text-sm font-medium text-gray-600">Keyword</th>
                <th className="px-6 py-3 text-left text-sm font-medium text-gray-600">Volumen de Búsqueda</th>
              </tr>
            </thead>
            <tbody>
              {keywords.map((keyword, index) => (
                <tr
                  key={index}
                  className="border-t hover:bg-teal-50 transition-colors duration-300"
                >
                  <td className="px-6 py-4 text-sm text-gray-800">{keyword.Name}</td>
                  <td className="px-6 py-4 text-sm text-gray-800">{keyword.SearchVolume}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default Dashboard;