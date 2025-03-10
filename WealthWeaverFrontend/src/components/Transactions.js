import React from "react";
import { MinusCircleIcon, PlusCircleIcon } from "@heroicons/react/outline";

/**
 * Provides a table to show a list of transactions
 * @param {transactions} @see mock-data/transactions.json
 */
export default function Transactions({ transactions }) {
  return (
    <ul className="text-black pb-2 dark:text-white">
      {transactions.map((transaction) => (
        <li
          className="flex items-center my-2 border-b border-dashed border-gray-400 p-2 gap-4 last:border-none"
          key={transaction.amount}
        >
          {transaction.isCredit ? (
            <PlusCircleIcon className="w-6 h-6 text-green-500" />
          ) : (
            <MinusCircleIcon className="w-6 h-6 text-red-500" />
          )}
          <div className="flex flex-col flex-grow">
            <p>
              {transaction.name} - {transaction.location}
            </p>
            <span className="text-xs text-gray-500">
              {transaction.postedDate}
            </span>
          </div>
          {transaction.isCredit ? (
            <p className="text-green-500">{transaction.amount}</p>
          ) : (
            <p className="text-red-500">{transaction.amount}</p>
          )}
        </li>
      ))}
    </ul>
  );
}
