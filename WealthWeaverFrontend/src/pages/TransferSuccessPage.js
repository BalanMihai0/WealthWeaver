import React from "react";
import Header from "../components/Header";
import { CheckCircleIcon } from "@heroicons/react/solid";
import receipt from "../mock-data/transferReceipt.json";

/**
 * This provides the transfer success screen
 * @see mock-data/transferReceipt.json
 */
export default function TransferSuccessPage() {
  return (
    <main className="px-4 py-8">
      <Header active={"transfer"} />
      <div className="flex justify-center flex-col items-center">
        <div className="flex flex-col items-center self-center w-full md:w-1/2">
          <h1 className="text-gray-500 mb-4">TRANSFER SUCCEEDED</h1>
          <div className="flex flex-col mb-8 bg-white w-full shadow-lg rounded-lg p-8 dark:bg-gray-700 dark:text-white">
            <div className="self-center">
              <CheckCircleIcon className="w-20 h-20 text-green-500" />
            </div>
            <div className="py-8 flex flex-col gap-8">
              <div className="flex items-center flex-col gap-4 lg:flex-row lg:text-left">
                <p className="flex-1">From Account </p>
                <p className="px-4 py-2 bg-gray-200 rounded-full dark:bg-gray-800">
                  {receipt.from}
                </p>
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <p className="flex-1">To Account </p>
                <p className="px-4 py-2 bg-gray-200 rounded-full dark:bg-gray-800">
                  {receipt.to}
                </p>
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <p className="flex-1">Amount</p>
                <p className="px-4 py-2 bg-gray-200 rounded-full dark:bg-gray-800">
                  {receipt.amount}
                </p>
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <p className="flex-1">Posted Date</p>
                <p className="px-4 py-2 bg-gray-200 rounded-full dark:bg-gray-800">
                  {receipt.postedDate}
                </p>
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <p className="flex-1">Memo</p>
                <p className="px-4 py-2 bg-gray-200 rounded-full dark:bg-gray-800">
                  {receipt.memo}
                </p>
              </div>
              <div className="flex justify-center">
                <button className="text-gray-500 hover:underline dark:text-gray-300">
                  Download Receipt
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
