'use client';

import React, { useState } from "react";

const Calculator: React.FC = () => {
    const [input, setInput] = useState<string>("");

    const handleButtonClick = (value: string) => {
        setInput((prev) => prev + value);
    };

    const handleClear = () => {
        setInput("");
    };

    const handleCalculate = () => {
        try {
            // Используем функцию eval с осторожностью
            const result = eval(input);
            setInput(result.toString());
        } catch (error) {
            setInput((error as Error).message);
        }
    };

    return (
        <div className="max-w-xs mx-auto mt-10 p-5 border rounded-2xl shadow-lg bg-white">
            <div className="mb-4 p-3 text-right border rounded text-xl bg-gray-100 min-h-[60px]">
                {input || "0"}
            </div>
            <div className="grid grid-cols-4 gap-3">
                {["7", "8", "9", "/",
                    "4", "5", "6", "*",
                    "1", "2", "3", "-",
                    "0", ".", "=", "+"].map((btn) => (
                    <button
                        key={btn}
                        onClick={() => btn === "=" ? handleCalculate() : handleButtonClick(btn)}
                        className="p-4 text-xl font-bold bg-gray-200 rounded hover:bg-gray-300 transition"
                    >
                        {btn}
                    </button>
                ))}
                <button
                    onClick={handleClear}
                    className="col-span-4 p-4 text-xl font-bold bg-red-400 text-white rounded hover:bg-red-500 transition"
                >
                    Очистить
                </button>
            </div>
        </div>
    );
};

export { Calculator };
export default Calculator;
