import React from "react";
import { LibraryIcon } from "@heroicons/react/solid";
import { Link } from "react-router-dom";

/**
 * Login Page provides username/password based form template
 *
 */
export default function LoginPage() {
  return (
    <main className="py-16">
      <div className="flex justify-center">
        <div className="flex flex-col bg-white shadow-lg rounded-lg dark:bg-gray-700 dark:text-white md:w-1/2">
          <div className="bg-blue-500 rounded-t-lg flex flex-col justify-center items-center p-8">
            <LibraryIcon className="w-20 h-20 text-white" />
            <h1 className="text-white font-bold text-2xl">Wealth Weaver - Personal Finance Management</h1>
          </div>
          <div className="p-8 lg:w-1/2 md:self-center">
            <h1 className="text-gray-500 text-center">PLEASE LOGIN TO ACCESS THE PLATFORM</h1>
            <div className="py-8 flex flex-col gap-8">
              <input
                type="text"
                placeholder="Username"
                className="px-4 py-2 bg-gray-200 rounded-full text-blue-500 dark:bg-gray-800 focus:outline-blue-500"
              />
              <div>
                <input
                  type="password"
                  placeholder="Password"
                  className="px-4 py-2 bg-gray-200 rounded-full w-full text-blue-500 dark:bg-gray-800 focus:outline-blue-500"
                />
                <p className="text-xs my-2 text-blue-500 text-center cursor-pointer hover:underline">
                  Forgot Password?
                </p>
              </div>
              <Link
                to="/accounts"
                className="bg-blue-500 px-4 py-2 rounded-full text-white text-center"
              >
                LOGIN
              </Link>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
